namespace CentralSystem.FlowManager.Messaging.Implementation.Handlers.FlowProcessing.TimeoutObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging;
    using CentralSystem.Messaging.FlowManager;

    /// <summary>
    /// Base event class for Flow timeout identifiers
    /// </summary>
    public abstract class BaseFlowActivityTimeoutIdentifier : BaseFlowTimeoutIdentifier, IFlowStepInstance 
    {

        #region Properties

        /// <summary>
        /// Step instance ID
        /// </summary>
        public int StepInstanceID { get; set; }

        /// <summary>
        /// Timer identifier
        /// </summary>
        public string TimerIdentifier { get; set; }

        #endregion

    }
}
