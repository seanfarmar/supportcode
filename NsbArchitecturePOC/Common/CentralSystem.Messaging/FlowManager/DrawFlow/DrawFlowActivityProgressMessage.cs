namespace CentralSystem.Messaging.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Draw flow activity result message
    /// </summary>
    public sealed class DrawFlowActivityProgressMessage : FlowActivityProgressMessage
    {

        #region Constructors

        /// <summary>
        /// Default contractor
        /// </summary>
        public DrawFlowActivityProgressMessage()
        {
            RootObjectType = RootObjectTypes.Draw;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Root object type
        /// </summary>
        public sealed override string RootObjectType
        {
            get
            {
                return base.RootObjectType;
            }
            set
            {
                if (value != RootObjectTypes.Draw)
                {
                    throw new NotSupportedException("Current message supports only for Draw Flow");
                }
                base.RootObjectType = value;
            }
        }

        /// <summary>
        /// Root Object ID
        /// </summary>
        public int DrawID
        {
            get
            {
                return RootObjectID;
            }
            set
            {
                RootObjectID = value;
            }
        }

        #endregion

    }
}
