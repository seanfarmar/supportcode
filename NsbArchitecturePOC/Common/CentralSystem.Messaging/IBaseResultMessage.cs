namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for identification of base result message
    /// </summary>
    public interface IBaseResultMessage : IBaseMessage 
    {

        #region Properties

        /// <summary>
        /// Error code
        /// </summary>
        int ErrorCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        string ErrorMessage { get; set; }

        #endregion

    }
}
