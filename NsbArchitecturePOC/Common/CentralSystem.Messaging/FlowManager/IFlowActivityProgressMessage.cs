namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for identification of flow activity progress message
    /// </summary>
    public interface IFlowActivityProgressMessage : IFlowActivityCommand, IBaseMessage 
    {

        #region Properties

        /// <summary>
        /// Progress percentage
        /// </summary>
        decimal ProgressPercentage { get; set; }

        /// <summary>
        /// Progress event name
        /// </summary>
        string ProgressEventName { get; set; }

        /// <summary>
        /// Progress data
        /// </summary>
        string ProgressData { get; set; }

        #endregion

    }
}
