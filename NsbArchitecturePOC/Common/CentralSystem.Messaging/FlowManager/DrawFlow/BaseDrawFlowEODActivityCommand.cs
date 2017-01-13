namespace CentralSystem.Messaging.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base activity command for EOD draw activities
    /// </summary>
    public abstract class BaseDrawFlowEODActivityCommand : BaseDrawFlowActivityCommand, IBusinessDateMessage
    {

        #region Properties

        /// <summary>
        /// True - first instance of EOD step definition
        /// </summary>
        public bool IsFirst { get; set; }

        /// <summary>
        /// CDC
        /// </summary>
        public int CDC { get; set; }

        #endregion

    }
}
