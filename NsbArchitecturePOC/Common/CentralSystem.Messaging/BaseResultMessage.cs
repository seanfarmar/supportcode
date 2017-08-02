namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Basic class for result message
    /// </summary>
    public class BaseResultMessage : BaseMessage, IBaseResultMessage  
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseResultMessage()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Error code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion

    }
}
