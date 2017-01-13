using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.DataObjects.ValueObjects
{
    /// <summary>
    /// Class provides information about error details
    /// </summary>
    public class ErrorStatus
    {
        #region Constructor

        /// <summary>
        /// Default no error status
        /// </summary>
        public ErrorStatus()
        {
            ErrorMessage = String.Empty;
        }

        /// <summary>
        /// Set error status to object
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="errorMessage">Error message</param>
        public ErrorStatus(int errorCode, string errorMessage)
        {
            SetErrorStatus(errorCode, errorMessage);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Validation flag
        /// </summary>
        public virtual bool IsValid
        {
            get
            {
                return (ErrorCode == 0);
            }
        }

        /// <summary>
        /// Error code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion

        #region Properties Populated During Business Flow

        /// <summary>
        /// Internal error
        /// </summary>
        public ErrorStatus InternalErrorStatus { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set error status to object
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="errorMessage">Error message</param>
        public void SetErrorStatus(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Set internal error status to object
        /// </summary>
        /// <param name="internalErrorCode">Internal error code</param>
        /// <param name="internalErrorMessage">Internal error message</param>
        public virtual void SetInternalErrorStatus(int internalErrorCode, string internalErrorMessage)
        {
            InternalErrorStatus = new ErrorStatus();
            InternalErrorStatus.SetErrorStatus(internalErrorCode, internalErrorMessage);
        }

        /// <summary>
        /// Helper function to copy all error information
        /// </summary>
        /// <param name="source">Source validation result</param>
        public void CopyStatusFrom(ErrorStatus source)
        {
            ErrorCode = source.ErrorCode;
            ErrorMessage = source.ErrorMessage;
            InternalErrorStatus = null;
            if (source.InternalErrorStatus != null)
            {
                InternalErrorStatus = new ErrorStatus();
                InternalErrorStatus.CopyStatusFrom(source.InternalErrorStatus);
            }
        }

        /// <summary>
        /// Internal status
        /// </summary>
        public void ResetStatus()
        {
            ErrorCode = 0;
            ErrorMessage = string.Empty;
            InternalErrorStatus = null;
        }

        #endregion

    }
}
