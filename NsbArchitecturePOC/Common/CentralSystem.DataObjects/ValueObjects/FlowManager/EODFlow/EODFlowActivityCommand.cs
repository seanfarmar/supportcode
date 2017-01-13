namespace CentralSystem.DataObjects.ValueObjects.FlowManager.EODFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// EOD Flow activity command definition
    /// </summary>
    public class EODFlowActivityCommand : FlowActivityCommand
    {

        #region Properties

        /// <summary>
        /// Business date CDC number based on Root Object ID
        /// </summary>
        public int CDC { get; set; }

        #endregion

    }
}
