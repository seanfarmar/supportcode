namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Destination data center command command interface
    /// Current interface reused to mark commands as upload to specific data center
    /// </summary>
    public interface IDestinationDataCenterCommand : IBaseMessage
    {

        #region Properties

        /// <summary>
        /// Destination data center
        /// </summary>
        int DestinationDataCenter { get; set; }

        #endregion

    }
}
