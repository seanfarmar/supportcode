namespace CentralSystem.FlowManager.Messaging.Implementation.Handlers.FlowProcessing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.DataObjects.ValueObjects;
    using CentralSystem.DataObjects.ValueObjects.FlowManager;
    using CentralSystem.FlowManager.Messaging.Activities;
    using CentralSystem.FlowManager.Messaging.Implementation.Handlers.FlowProcessing.TimeoutObjects;
    using CentralSystem.Framework.NServiceBus.Exceptions;
    using CentralSystem.Messaging;
    using CentralSystem.Messaging.FlowManager;
    using CentralSystem.Messaging.Implementation.Handlers;
    using NServiceBus;
    using NServiceBus.Saga;
    using CentralSystem.Messaging.Implementation.Mapping;
    using CentralSystem.Messaging.Implementation.Mapping.FlowManager;
    using CentralSystem.Reporting.Messaging;

    /// <summary>
    /// Saga business process to handle flow instance
    /// </summary>
    public sealed class FlowInstanceSaga : BaseSaga<FlowInstanceSagaDataPOC>
        , IHandleSagaNotFound
        , IAmStartedByMessages<StartNewFlowCommand>
        , IHandleMessages<IFlowActivityResultMessage>
        //Timeout Manager events
        , IHandleTimeouts<FlowActivityCommandRetryTimeoutIdentifier>
    {

        #region Override Methods

        /// <summary>
        /// Mapping to find a saga
        /// </summary>
        /// <param name="mapper">Mapper</param>
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<FlowInstanceSagaDataPOC> mapper)
        {
            //Base index is flow instance ID - according to requirement not only draw based workflow process
            mapper.ConfigureMapping<IFlowActivityResultMessage>(sagaMessage => sagaMessage.FlowInstanceID)
                .ToSaga(sagaData => sagaData.FlowInstanceID);
        }

        #endregion

        #region IHandleSagaNotFound Members

        /// <summary>
        /// Saga not found implementation
        /// </summary>
        /// <param name="message">Message object</param>
        void IHandleSagaNotFound.Handle(object message)
        {
            //The reason for not error:
            //for already completed flows - ordered command/retry timeouts are valid scenarios
            //CentralLogger.Warn(string.Format(Messages.INT_FLOW_INSTANCE_SAGA_NOT_FOUND, message.GetType().ToString()),
            //    MessageCodes.INT_FLOW_INSTANCE_SAGA_NOT_FOUND,
            //    new RelatedObjectsLogContainer().AddObject("Message", message));
        }

        #endregion

        #region IHandleMessages<StartNewFlowActivityCommand> Members

        /// <summary>
        /// Handle start flow activity scenario.
        /// Modes:
        /// - Default - send result message
        /// - Cancel to send result message (command line tool)
        /// - Return callback return code (to commands gateway WCF service)
        /// </summary>
        /// <param name="message">Start command</param>
        public void Handle(StartNewFlowCommand message)
        {
            FlowActivityResultMessage activityResultMessage = new FlowActivityResultMessage();

            //Save external root object identifiers
            Data.BrandID = message.BrandID; //Flow creator should validate provider brand ID in the header
            Data.RootObjectID = message.RootObjectID;
            Data.RootObjectType = message.RootObjectType;
            Data.FlowInstanceID = message.RootObjectID;

            try
            {
                //Map all headers to result message
                new FlowActivityCommandMappingBuilder().ExecuteMapping(message, activityResultMessage);

                //Save saga state
                Data.Version = message.NewRootObjectID;

                //Handle and process flow result
                HandleAndProcessFlowResultAndThrowIfInvalid();

            }
            catch (Exception ex)
            {
                LogAndThrowExceptionWithSecondLayerRetryDecision(message, ex,
                    true); //On any exception to cancel second level retry mechanism.
            }

            //Send result message if flag undefined. 
            //Default is false, initiator is previous flow activity "Instantiate ..." (draw or EOD)
            //Scenarios for defined flag:
            // - Command line initiator to create flow
            // - Maintenance EOD command handler which executed also from command line
            if (!message.DoNotSendResultMessage)
            {
                Bus.SendResultMessage(activityResultMessage);
            }

            //Send result code.
            //Used in the WCF gateway scenario to send callback indication about created flow
            if (message.ReturnResultCode)
            {
                Bus.Return<int>(activityResultMessage.ErrorCode);
            }
        }

        #endregion

        #region IHandleMessages<IFlowActivityResultMessage> Members

        /// <summary>
        /// Handler of flow activity result
        /// </summary>
        /// <param name="message">Flow activity result message</param>
        public void Handle(IFlowActivityResultMessage message)
        {
            ActivateOptimisticLock();
        }

        #endregion

        #region IHandleTimeouts<FlowActivityCommandRetryTimeoutIdentifier> Members

        /// <summary>
        /// Flow activity command retry timeout event
        /// </summary>
        /// <param name="state">Command retry timeout identifier</param>
        public void Timeout(FlowActivityCommandRetryTimeoutIdentifier state)
        {
            try
            {
                //Handle and process flow result
                HandleAndProcessFlowResultAndThrowIfInvalid();
            }
            catch (Exception ex)
            {
                LogAndThrowExceptionWithSecondLayerRetryDecision(state, ex,
                    false); //Don't cancel second level retry mechanism except to special business case.
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handle flow result - base process for any BL flaw result
        /// and throw exception if invalid response
        /// </summary>
        private void HandleAndProcessFlowResultAndThrowIfInvalid()
        {
            ActivateOptimisticLock();

            //Send flow activity commands for execution
            SendFlowActivityCommands();

            //Order all timeouts
            RequestFlowTimeoutEvents();
        }

        private void ActivateOptimisticLock()
        {
            //******************************************************************************************************************
            //The changing of version has effect to optimistic lock implementation which integrated with transaction mechanism.
            //
            //The transaction scope includes: 
            // - Changes in the business tables.
            // - Transport send commands / messages / timeout requests.
            // - Save saga state object.
            //
            //By design - the column "Version" connected to optimistic lock implementation.
            //In case if version value was changed by other process / handler according to same saga instance (flow instance)
            // and in the context of current transaction was persisted business decisions in the database,
            // all transaction changes should be roll backed and re-executed.
            //
            //Business layer defined 2 types of responses:
            // - Indication that was not changed any flow instance state (flag NoFlowChanges)
            // - Instructions to initiate out coming messages: order time reach event timeouts, 
            //    order command execution / retry timeouts and send commands for execution.
            //In both scenarios - should be initiated optimistic lock mechanism by version changing.
            //******************************************************************************************************************
            if (Data.Version == System.Int32.MaxValue)
            {
                Data.Version = 1;
            }
            else
            {
                Data.Version = Data.Version + 1;
            }

        }

        /// <summary>
        /// Initialize flow instance based properties from Saga
        /// </summary>
        /// <param name="flowInstanceCommand">Flow instance command</param>
        private void SetFlowInstanceHeaders(IFlowInstance flowInstanceCommand)
        {
            flowInstanceCommand.BrandID = Data.BrandID;
            flowInstanceCommand.FlowInstanceID = Data.FlowInstanceID;
            flowInstanceCommand.RootObjectType = Data.RootObjectType;
            flowInstanceCommand.RootObjectID = Data.RootObjectID;
        }

        /// <summary>
        /// Send flow activity commands for execution
        /// </summary>
        private void SendFlowActivityCommands()
        {
            IFlowActivityCommand drawActivityCommand = new GenerateHourlySignedTicketsCommand();

            SetFlowInstanceHeaders(drawActivityCommand);

            //Command headers based on business logic
            drawActivityCommand.StepInstanceID = Data.Version + 1;

            //Send activity command
            Bus.Send(drawActivityCommand);
        }

        /// <summary>
        /// Request flow activity command timeouts.
        /// For every next timeout the function is responsible to instantiate final instance of event (state) object with initialized custom properties.
        /// </summary>
        /// <param name="flowRequest">Business request</param>
        /// <param name="flowResult">Business response</param>
        private void RequestFlowTimeoutEvents()
        {
            RequestFlowSingleStepInstanceBasedTimeoutEvent<FlowActivityCommandRetryTimeoutIdentifier>();
        }

        /// <summary>
        /// Request specific flow instance step based timeout (command retry/timeout) event
        /// </summary>
        /// <typeparam name="TTimeoutEvent">Final event (state) instance</typeparam>
        /// <param name="flowRequest">Flow request</param>
        /// <param name="timeout">Information about timeout event</param>
        private void RequestFlowSingleStepInstanceBasedTimeoutEvent<TTimeoutEvent>()
            where TTimeoutEvent : BaseFlowActivityTimeoutIdentifier, new()
        {
            //Created final event (state) instance for timeout event
            TTimeoutEvent requestedFlowTimeoutEvent = new TTimeoutEvent();

            SetFlowInstanceHeaders(requestedFlowTimeoutEvent);

            //Timeout event headers based on business logic
            requestedFlowTimeoutEvent.StepInstanceID = Data.Version + 1;
            requestedFlowTimeoutEvent.TimerIdentifier = Guid.NewGuid().ToString();

            //The value used only in the logging of message
            requestedFlowTimeoutEvent.Timeout = TimeSpan.FromSeconds(5);

            RequestTimeout(requestedFlowTimeoutEvent.Timeout, requestedFlowTimeoutEvent);
        }

        /// <summary>
        /// Handle and throw exception to NServiceBus
        /// with logging.
        /// Also implemented override logic to cancel second layer retry mechanism according to
        /// input parameter or special business exception type.
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        /// <param name="sourceException">Source exception</param>
        /// <param name="forceThrowCancelRetryException">True - generate cancel retry exception</param>
        private void LogAndThrowExceptionWithSecondLayerRetryDecision<TMessage>(TMessage message, Exception sourceException, bool forceThrowCancelRetryException)
            where TMessage : IBaseMessage
        {
            //Logging
            //CentralLogger.Error(string.Format("Error on attempt to execute command '{0}'.", message.GetType().Name),
            //    CentralSystemException.GetErrorCode(sourceException), //Get source exception code
            //    sourceException, //Override exception
            //    new RelatedObjectsLogContainer().AddObject("Message", message));

            Exception throwException = sourceException;

            if (forceThrowCancelRetryException)
            {
                //On any exception to cancel second level retry mechanism.
                //In any case will be initiated first level retry mechanism and second level will be canceled.
                //Retry mechanism will be initiated by previous flow instance activity on timeout scenario.
                //Fail message can't be sending, this is part of transaction rollback scenario.
                throwException = new CancelBusSecondLevelRetryException(
                    string.Format("Cancel retry {0}", Data),
                    sourceException);
            }

            throw throwException;
        }

        #endregion

    }

}
