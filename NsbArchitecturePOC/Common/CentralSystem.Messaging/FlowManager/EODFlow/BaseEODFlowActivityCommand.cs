namespace CentralSystem.Messaging.FlowManager.EODFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base command for draw flow activity
    /// </summary>
    public abstract class BaseEODFlowActivityCommand : BaseFlowActivityCommand, IBusinessDateMessage
    {

        #region Constructors

        /// <summary>
        /// Default contractor
        /// </summary>
        protected BaseEODFlowActivityCommand()
        {
            RootObjectType = RootObjectTypes.CDC;
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
                if (value != RootObjectTypes.CDC)
                {
                    throw new NotSupportedException("Current message supports only for EOD Flow");
                }
                base.RootObjectType = value;
            }
        }

        /// <summary>
        /// CDC based on Root Object ID
        /// </summary>
        public int CDC 
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
