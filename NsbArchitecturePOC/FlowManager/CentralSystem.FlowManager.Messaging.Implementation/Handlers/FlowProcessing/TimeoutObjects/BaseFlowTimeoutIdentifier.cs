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
    public class BaseFlowTimeoutIdentifier : BaseMessage, IFlowInstance
    {

        #region Properties

        /// <summary>
        /// Root object type - for header initialization
        /// </summary>
        public string RootObjectType { get; set; }

        /// <summary>
        /// Root object ID - for header initialization
        /// </summary>
        public int RootObjectID { get; set; }

        /// <summary>
        /// Flow instance ID - for header initialization
        /// </summary>
        public int FlowInstanceID { get; set; }

        /// <summary>
        /// Timeout value
        /// The value used only in the logging of message.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        #endregion

    }
}
