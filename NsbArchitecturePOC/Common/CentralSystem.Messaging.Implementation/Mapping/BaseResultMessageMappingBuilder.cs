namespace CentralSystem.Messaging.Implementation.Mapping
{
    using CentralSystem.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonMessageContracts = CentralSystem.Messaging;
    using CommonValueObjects = CentralSystem.DataObjects.ValueObjects;

    /// <summary>
    /// Mapping builder class is responsible for BaseResultMessage mapping:
    /// - From request to response Message objects
    /// - From exception to response Message objects
    /// - From base response value object to base Message objects
    /// </summary>
    public sealed class BaseResultMessageMappingBuilder
    {

        #region Methods

        /// <summary>
        /// Mapping from exception to base result message objects
        /// </summary>
        /// <param name="source">Exception</param>
        /// <param name="destination">Base result message</param>
        public void ExecuteMappingFromException(Exception source, CommonMessageContracts.IBaseResultMessage destination)
        {
            destination.ErrorCode = CentralSystemException.DEFAULT_ERROR_CODE;
            destination.ErrorMessage = CentralSystemException.DEFAULT_ERROR_MESSAGE;
            CentralSystemException csException = source as CentralSystemException;
            if (csException != null && csException.ErrorCode != 0)
            {
                destination.ErrorCode = csException.ErrorCode;
                destination.ErrorMessage = csException.Message;
            }
        }

        /// <summary>
        /// Mapping from value to result message object
        /// </summary>
        /// <param name="source">Value object</param>
        /// <param name="destination">Result message object</param>
        public void ExecuteMappingFromErrorStatus(CommonValueObjects.ErrorStatus source, CommonMessageContracts.IBaseResultMessage destination)
        {
            destination.ErrorCode = source.ErrorCode;
            destination.ErrorMessage = source.ErrorMessage;
        }

        #endregion

    }
}
