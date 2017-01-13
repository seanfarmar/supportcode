namespace CentralSystem.FlowManager.Messaging.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.FlowManager;
    using NServiceBus;

    /// <summary>
    /// Command for temp configuration of automatic steps for execution
    /// </summary>
    public sealed class DoNothingFlowCommand : BaseFlowActivityCommand, ICommand
    {

        #region Properties

        /// <summary>
        /// Sleep interval: long execution scenario.
        /// </summary>
        public TimeSpan SleepInterval
        {
            get;
            set;
        }

        /// <summary>
        /// True throw exception: NSB unhandled exception scenario.
        /// </summary>
        public bool ThrowUnhandledException
        {
            get;
            set;
        }

        /// <summary>
        /// Throw cancel retry exception (cancel second level retry mechanism scenario)
        /// in case second layer retry count greater than current setting.
        /// Actual only if defined ThrowUnhandledException flag.
        /// </summary>
        public int ThrowCancelRetryExceptionAfterSLRetryAttemptsCount
        {
            get;
            set;
        }

        #endregion

    }
}
