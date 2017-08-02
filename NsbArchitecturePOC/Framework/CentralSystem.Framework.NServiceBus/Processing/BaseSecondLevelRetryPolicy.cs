namespace CentralSystem.Framework.NServiceBus.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Exceptions;
    using global::NServiceBus;

    /// <summary>
    /// Base handler policy for second level retry.
    /// Registration should be executed by BusConfigurationExtensions.
    /// </summary>
    public abstract class BaseSecondLevelRetryPolicy
    {

        #region Methods

        /// <summary>
        /// Custom retry policy abstract function definition
        /// </summary>
        /// <param name="transportMessage">Transport message</param>
        /// <returns>Zero - no next retry</returns>
        public abstract TimeSpan Execute(TransportMessage transportMessage);

        /// <summary>
        /// Check if retry should be canceled by CancelBusRetryException
        /// </summary>
        /// <param name="transportMessage">Transport message</param>
        /// <returns>True - retry should be canceled</returns>
        protected bool CheckIfCanceledByException(TransportMessage transportMessage)
        {
            if (transportMessage.Headers[NSBCommonSettings.Headers.NSB_EXCEPTION_INFO_TYPE] == typeof(CancelBusSecondLevelRetryException).FullName)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find exception data in the 
        /// </summary>
        /// <param name="transportMessage">Transport message</param>
        /// <returns>Exception data</returns>
        protected string GetExceptionData(TransportMessage transportMessage)
        {
            string exceptionInfo = null;
            transportMessage.Headers.TryGetValue(NSBCommonSettings.Headers.NSB_EXCEPTION_INFO_STACK_TRACE, out exceptionInfo);
            return exceptionInfo;
        }

        /// <summary>
        /// Get numbers of retry from header
        /// </summary>
        /// <param name="message">Transport message</param>
        /// <returns>Retries count</returns>
        protected int GetNumberOfRetries(TransportMessage message)
        {
            string value;
            if (message.Headers.TryGetValue(Headers.Retries, out value))
            {
                int i;
                if (int.TryParse(value, out i))
                {
                    return i;
                }
            }
            return 0;
        }

        #endregion

    }
}
