namespace CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Hourly activity type.
    /// Required flag based enumeration.
    /// </summary>
    [Flags]
    public enum HourlyActivityTypes
    {
        /// <summary>
        /// Regular activity type - default
        /// </summary>
        HourlyRegular = 0,

        /// <summary>
        /// First time range
        /// </summary>
        FirstTime = 1,

        /// <summary>
        /// Last time range before EOD
        /// </summary>
        LastHourBeforeEOD = 2,

        /// <summary>
        /// Last time on Draw Date
        /// </summary>
        LastTime = 4,
    }
}
