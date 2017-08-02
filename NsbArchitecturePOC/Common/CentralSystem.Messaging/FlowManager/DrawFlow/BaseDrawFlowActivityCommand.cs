namespace CentralSystem.Messaging.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base command for draw flow activity
    /// </summary>
    public abstract class BaseDrawFlowActivityCommand : BaseFlowActivityCommand
    {

        #region Constructors

        /// <summary>
        /// Default contractor
        /// </summary>
        protected BaseDrawFlowActivityCommand()
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
        /// Draw ID based on Root Object ID
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
