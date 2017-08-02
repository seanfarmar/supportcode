namespace CentralSystem.Messaging.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base activity command for draw hourly activities
    /// </summary>
    public abstract class BaseDrawFlowHourlyActivityCommand : BaseDrawFlowActivityCommand
    {

        #region Properties

        /// <summary>
        /// Hourly type
        /// </summary>
        public HourlyActivityTypes HourlyType { get; set; }

        /// <summary>
        /// Previous hourly identifier
        /// </summary>
        public Nullable<DateTime> PreviousTimeIdentifier { get; set; }

        /// <summary>
        /// Current Hourly identifier
        /// </summary>
        public DateTime CurrentTimeIdentifier { get; set; }

        /// <summary>
        /// CDC - defined only for Last Hour Before EOD hourly type
        /// </summary>
        public Nullable<int> CDC { get; set; }

        #endregion

    }
}
