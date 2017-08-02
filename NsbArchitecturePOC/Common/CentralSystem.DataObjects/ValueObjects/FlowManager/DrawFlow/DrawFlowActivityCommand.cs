namespace CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Draw Flow activity command definition
    /// </summary>
    public class DrawFlowActivityCommand : FlowActivityCommand
    {

        #region Properties

        /// <summary>
        /// Draw ID based on Root Object ID
        /// </summary>
        public int DrawID { get; set; }

        #endregion

    }
}
