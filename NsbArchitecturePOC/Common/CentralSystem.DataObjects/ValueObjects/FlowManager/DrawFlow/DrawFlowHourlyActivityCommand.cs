namespace CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for draw flow activity hourly command
    /// </summary>
    public class DrawFlowHourlyActivityCommand : DrawFlowActivityCommand
    {

        #region Properties

        /// <summary>
        /// Hourly type
        /// </summary>
        public HourlyActivityTypes HourlyType { get; set; }

        /// <summary>
        /// Previous time identifier
        /// </summary>
        public Nullable<DateTime> PreviousTimeIdentifier { get; set; }

        /// <summary>
        /// Current time identifier
        /// </summary>
        public DateTime CurrentTimeIdentifier { get; set; }

        /// <summary>
        /// CDC - defined only for Last Hour Before EOD hourly type
        /// </summary>
        public Nullable<int> CDC { get; set; }

        #endregion

    }
}
