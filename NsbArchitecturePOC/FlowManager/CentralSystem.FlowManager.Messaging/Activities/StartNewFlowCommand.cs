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
    /// Start message for flow saga process (flow instance)
    /// </summary>
    public class StartNewFlowCommand : BaseFlowActivityCommand, ICommand 
    {

        #region Properties

        /// <summary>
        /// True - cancel send result command message.
        /// Used by command line tool.
        /// </summary>
        public bool DoNotSendResultMessage { get; set; }

        /// <summary>
        /// True - return result code.
        /// Used by WCF gateway service.
        /// </summary>
        public bool ReturnResultCode { get; set; }

        /// <summary>
        /// Flow definition id.
        /// If NULL - creator implement the logic to get flow definition id from root object type based configuration.
        /// </summary>
        public Nullable<int> FlowDefinitionID { get; set; }

        /// <summary>
        /// New root object ID
        /// </summary>
        public int NewRootObjectID { get; set; }

        #endregion

    }
}
