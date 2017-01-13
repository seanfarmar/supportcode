namespace CentralSystem.DataObjects.ValueObjects.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Progress activity progress data
    /// </summary>
    public sealed class FlowActivityProgressEventArgs
    {

        #region Constructors

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public FlowActivityProgressEventArgs()
        {

        }

        /// <summary>
        /// Clone constructor
        /// </summary>
        /// <param name="source">Source object</param>
        public FlowActivityProgressEventArgs(FlowActivityProgressEventArgs source)
        {
            ProgressPercentage = source.ProgressPercentage;
            ProgressEventName = source.ProgressEventName;
            ProgressEventID = source.ProgressEventID;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Progress percentage
        /// </summary>
        public decimal ProgressPercentage { get; set; }

        /// <summary>
        /// Progress event name
        /// </summary>
        public string ProgressEventName { get; set; }

        /// <summary>
        /// Progress percentage
        /// </summary>
        public string ProgressEventID { get; set; }

        /// <summary>
        /// Progress data
        /// </summary>
        public string ProgressData { get; set; }

        #endregion

    }
}
