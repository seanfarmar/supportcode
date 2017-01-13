using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.DataObjects.ValueObjects
{
    /// <summary>
    /// Basic class for response :
    /// - Don't need a properties from request header. The reason - service layer is responsible to return values from base request object.
    /// </summary>
    public class BaseResponse : ErrorStatus 
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseResponse()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="errorStatus">Error status</param>
        public BaseResponse(ErrorStatus errorStatus)
        {
            ErrorCode = errorStatus.ErrorCode;
            ErrorMessage = errorStatus.ErrorMessage;
        }

        #endregion

    }

}
