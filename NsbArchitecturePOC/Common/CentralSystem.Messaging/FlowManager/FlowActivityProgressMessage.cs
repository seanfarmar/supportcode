namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NServiceBus;

    /// <summary>
    /// Flow activity progress message
    /// </summary>
    public class FlowActivityProgressMessage : BaseMessage, IMessage, IFlowActivityProgressMessage
    {

        #region Members

        /// <summary>
        /// Root object type
        /// </summary>
        private string m_rootObjectType;

        #endregion

        #region Properties

        /// <summary>
        /// Flow instance ID
        /// </summary>
        public int FlowInstanceID { get; set; }

        /// <summary>
        /// Root object type
        /// </summary>
        public virtual string RootObjectType
        {
            get
            {
                return m_rootObjectType;
            }
            set
            {
                m_rootObjectType = value;
            }
        }

        /// <summary>
        /// Root object ID
        /// </summary>
        public int RootObjectID { get; set; }

        /// <summary>
        /// Step instance ID
        /// </summary>
        public int StepInstanceID { get; set; }

        /// <summary>
        /// Progress percentage
        /// </summary>
        public decimal ProgressPercentage { get; set; }

        /// <summary>
        /// Progress event name
        /// </summary>
        public string ProgressEventName { get; set; }

        /// <summary>
        /// Progress data
        /// </summary>
        public string ProgressData { get; set; }

        #endregion

    }
}
