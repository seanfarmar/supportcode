namespace CentralSystem.FlowManager.Messaging.Implementation.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Configuration;
    using CentralSystem.Framework.NServiceBus.Exceptions;
    using CentralSystem.Framework.NServiceBus.Processing;
    using NServiceBus;

    /// <summary>
    /// Flow manager application second level custom retry policy implementation
    /// </summary>
    internal sealed class FMSecondLevelRetryPolicy : BaseSecondLevelRetryPolicy
    {

        #region Methods

        /// <summary>
        /// Flow manager second level custom retry policy implementation
        /// </summary>
        /// <param name="transportMessage">Transport message</param>
        /// <returns>Zero - no next retry</returns>
        public override TimeSpan Execute(TransportMessage transportMessage)
        {
            //Default - no retry
            TimeSpan resultRetryInterval = TimeSpan.MinValue;
            int numbersOfRetry = GetNumberOfRetries(transportMessage);

            if (CheckIfCanceledByException(transportMessage))
            {
                resultRetryInterval = TimeSpan.MinValue;
            }
            else
            {
                if (numbersOfRetry < 2)
                {
                    resultRetryInterval = TimeSpan.FromSeconds(10);
                }
                else if (numbersOfRetry < 2 + 10)
                {
                    resultRetryInterval = TimeSpan.FromSeconds(30);
                }
            }

            return resultRetryInterval;
        }

        #endregion

    }
}
