namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NServiceBus;

    /// <summary>
    /// Interface for identification of business date
    /// </summary>
    public interface IBusinessDateMessage : IBaseMessage
    {

        #region Properties

        /// <summary>
        /// CDC
        /// </summary>
        int CDC { get; set; }

        #endregion
    
    }
}
