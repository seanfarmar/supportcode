namespace CentralSystem.FlowManager.Messaging.Implementation.Handlers.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.FlowManager.Messaging.Activities;
    using CentralSystem.Framework.NServiceBus.Exceptions;
    using CentralSystem.Messaging.FlowManager;
    using CentralSystem.Messaging.Implementation.Handlers;
    using CentralSystem.Messaging.Implementation.Mapping;
    using CentralSystem.Messaging.Implementation.Mapping.FlowManager;
    using NServiceBus;

    /// <summary>
    /// Command handler to temp nothing execution.
    /// Used the following scenarios:
    /// 1. In the configuration of flow definitions if activity still undefined and not implemented - configure DoNothingFlowCommand.
    /// 2. In the testing of flow manager application for imitation o: success / fail result, unhandled exception, long time execution and timeout scenarios.
    /// </summary>
    public sealed class DoNothingFlowCommandHandler : BaseMessageHandler
        , IHandleMessages<DoNothingFlowCommand>
    {

        #region Private Classes

        /// <summary>
        /// Log tracer for testing of Bus Execution context
        /// </summary>
        private class BusExecutionContextItemLogTracker : IDisposable
        {

            #region IDisposable Members

            /// <summary>
            /// Log tracing
            /// </summary>
            public void Dispose()
            {
                //CentralLogger.Info("Test message from bus execution context item according to 'Do Nothing' flow activity",
                //    CommonMessageCodes.INT_NSB_DEFAULT_CODE);
            }

            #endregion
        }

        #endregion

        #region IHandleMessages<DoNothingFlowCommand> Members

        /// <summary>
        /// Stub implementation:
        /// - Delay logic
        /// - Error reply logic
        /// </summary>
        /// <param name="message">Command</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void Handle(DoNothingFlowCommand message)
        {
            if (BusExecutionContext != null)
            {
                BusExecutionContext.AddItem(new BusExecutionContextItemLogTracker());
            }

            if (message.SleepInterval > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(message.SleepInterval);
            }

            if (message.ThrowUnhandledException)
            {
                int retriesCount = GetNumberOfRetries();
                if (retriesCount > message.ThrowCancelRetryExceptionAfterSLRetryAttemptsCount)
                {
                    throw new CancelBusSecondLevelRetryException(string.Format("Test cancel retry exception. SL Retries: {0}", retriesCount));
                }
                throw new Exception(string.Format("Test exception. SL Retries: {0}", retriesCount));
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Get numbers of retry from header
        /// </summary>
        /// <returns>Retries count</returns>
        private int GetNumberOfRetries()
        {
            string value;
            if (Bus.CurrentMessageContext.Headers.TryGetValue(Headers.Retries, out value))
            {
                int i;
                if (int.TryParse(value, out i))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Imitate Out of Memory exception
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void ImitateOutOfMemoryException()
        {
            throw new OutOfMemoryException("Test Out Of Memory Exception");
        }

        #endregion

    }
}
