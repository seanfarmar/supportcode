namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NServiceBus;

    /// <summary>
    /// Flow activity result message
    /// </summary>
    public class FlowActivityResultMessage : BaseResultMessage, IMessage, IFlowActivityResultMessage 
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

        #endregion

    }
}
