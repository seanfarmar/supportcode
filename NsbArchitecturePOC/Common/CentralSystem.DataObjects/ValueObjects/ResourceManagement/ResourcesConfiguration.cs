namespace CentralSystem.DataObjects.ValueObjects.ResourceManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Resources configuration
    /// </summary>
    public sealed class ResourcesConfiguration
    {

        #region Properties

        /// <summary>
        /// True - enabled resource management
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Max workers per resource
        /// </summary>
        public byte MaxWorkersPerResource
        {
            get; 
            set;
        }

        /// <summary>
        /// NServiceBus setting - Max handle count by same machine
        /// </summary>
        public byte BusMaxHandleCountBySameServer
        {
            get;
            set;
        }

        /// <summary>
        /// NServiceBus setting - Max handle count by all servers
        /// </summary>
        public byte BusMaxHandleCountByAllServers
        {
            get;
            set;
        }

        /// <summary>
        /// NServiceBus setting - Defer timeout
        /// </summary>
        public TimeSpan BusDeferTimeout
        {
            get;
            set;
        }

        #endregion

    }
}
