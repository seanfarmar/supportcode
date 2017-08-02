namespace CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for flow activity EOD command
    /// </summary>
    public class DrawFlowEODActivityCommand : DrawFlowActivityCommand 
    {

        #region Properties

        /// <summary>
        /// True if is the first EOD report generation for this draw
        /// </summary>
        public bool IsFirst { get; set; }

        /// <summary>
        /// CDC
        /// </summary>
        public int CDC { get; set; }

        #endregion

    }
}
