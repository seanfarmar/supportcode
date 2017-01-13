namespace CentralSystem.Framework.NServiceBus.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.Exceptions;

    /// <summary>
    /// Central system NSB application exception class for initiation of
    /// second level retry mechanism cancellation.
    /// </summary>
    [Serializable]
    public sealed class CancelBusSecondLevelRetryException : CentralSystemException
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CancelBusSecondLevelRetryException(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CancelBusSecondLevelRetryException(int errorCode, string message, Exception innerException)
            : base(errorCode, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class with default error code.
        /// </summary>
        public CancelBusSecondLevelRetryException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class with default error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CancelBusSecondLevelRetryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class with default error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CancelBusSecondLevelRetryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class with default error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CancelBusSecondLevelRetryException(string message, CentralSystemException innerException)
            : base(innerException.ErrorCode, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CancelBusSecondLevelRetryException class with serialized data.
        /// </summary>
        /// <param name="info">he object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private CancelBusSecondLevelRetryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        #endregion

    }
}
