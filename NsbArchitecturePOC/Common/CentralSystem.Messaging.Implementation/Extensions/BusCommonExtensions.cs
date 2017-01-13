namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.DataObjects.ValueObjects;
    using CentralSystem.Messaging.FlowManager;
    using CentralSystem.Messaging.Implementation.Mapping;
    using CentralSystem.Messaging.Implementation.Mapping.FlowManager;

    /// <summary>
    /// Extension methods according to Bus
    /// </summary>
    public static class BusExtensions
    {

        #region Methods

        /// <summary>
        /// Send flow activity result message
        /// </summary>
        /// <param name="bus">Bus</param>
        /// <param name="flowActivityResultMessage">Flow activity result message</param>
        public static void SendResultMessage(this ISendOnlyBus bus, IFlowActivityResultMessage flowActivityResultMessage)
        {
            //Send result message only in case if not maintenance mode
            bus.Send(flowActivityResultMessage);
        }

        /// <summary>
        /// Send flow activity result message for idempotent scenario except to following conditions:
        /// - Cancel to send error result command if was committed at least one transaction
        /// - Cancel to send error result command if was detected idempotent request
        /// </summary>
        /// <param name="bus">Bus</param>
        /// <param name="flowActivityResultMessage">Flow activity result message</param>
        /// <param name="requestContext">Request context</param>
        public static void SendResultMessageWithIdempotencyVerification(this ISendOnlyBus bus, IFlowActivityResultMessage flowActivityResultMessage,
            RequestContext requestContext)
        {
            //Verification is actual only for error command result
            if (flowActivityResultMessage.ErrorCode != 0
                && (requestContext.CommittedTransactionCounter > 0 || requestContext.DetectedIdempotentRequestWasCommited))
            {
                return;

            }

            bus.SendResultMessage(flowActivityResultMessage);

        }

        /// <summary>
        /// Execute "DoNothing" command.
        /// This scenario used:
        /// 1. Dummy handler implementation
        /// 2. Start developing stage.
        /// 3. If command should be removed in next versions. Example - confirmation enable activities.
        /// </summary>
        /// <param name="bus">Bus</param>
        /// <param name="message">Command message</param>
        public static void DoNothingActivity(this ISendOnlyBus bus, IFlowActivityCommand message)
        {
            IFlowActivityResultMessage activityResultMessage = new FlowActivityResultMessage();

            try
            {
                //Map all headers
                new FlowActivityCommandMappingBuilder().ExecuteMapping(message, activityResultMessage);

                //Prepare result
                activityResultMessage.ErrorCode = 0;
                activityResultMessage.ErrorMessage = "Do Nothing Activity";

            }
            catch (Exception ex)
            {
                new BaseResultMessageMappingBuilder().ExecuteMappingFromException(ex, activityResultMessage);

            }

            bus.SendResultMessage(activityResultMessage);

        }

        #endregion

    }
}
