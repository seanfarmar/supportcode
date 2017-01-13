using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Exceptions
{
    /// <summary>
    /// Invalid Action Exception
    /// </summary>
    [Serializable]
    public class InvalidActionException : Exception
    {
        #region Members

        /// <summary>
        /// Default central system execution code
        /// </summary>
        public const int DEFAULT_ERROR_CODE = -2;

        /// <summary>
        /// Default central system execution message
        /// </summary>
        public const string DEFAULT_ERROR_MESSAGE = "General execution error";

        /// <summary>
        /// Key for serialization logic
        /// </summary>
        private const string ERROR_CODE_SERIALIZATION_KEY = "ErrorCode";

        /// <summary>
        /// Error code member
        /// </summary>
        private readonly int m_ErrorCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CentralSystemException class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidActionException(int errorCode, string message)
            : base(message)
        {
            m_ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the CentralSystemException class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidActionException(int errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            m_ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the CentralSystemException class with default error code.
        /// </summary>
        public InvalidActionException()
            : base(DEFAULT_ERROR_MESSAGE)
        {
            m_ErrorCode = DEFAULT_ERROR_CODE;
        }

        /// <summary>
        /// Initializes a new instance of the CentralSystemException class with default error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidActionException(string message)
            : base(message)
        {
            m_ErrorCode = DEFAULT_ERROR_CODE;
        }

        /// <summary>
        /// Initializes a new instance of the CentralSystemException class with default error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidActionException(string message, Exception innerException)
            : base(message, innerException)
        {
            m_ErrorCode = DEFAULT_ERROR_CODE;
        }

        /// <summary>
        /// Initializes a new instance of the CentralSystemException class with serialized extended custom data:
        /// - Error code
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected InvalidActionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_ErrorCode = info.GetInt32(ERROR_CODE_SERIALIZATION_KEY);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Exception error code.
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return m_ErrorCode;
            }
        } 

        #endregion

        #region Public Methods

        /// <summary>
        /// To add extended information about exception error code.
        /// </summary>
        /// <returns>Full exception information in text format</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format("\nError code is {0}.", this.ErrorCode.ToString());
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the System.Runtime.Serialization.SerializationInfo with extended custom information about the exception:
        /// - Error code
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(ERROR_CODE_SERIALIZATION_KEY, this.ErrorCode);
        }

        #endregion
    }
}
