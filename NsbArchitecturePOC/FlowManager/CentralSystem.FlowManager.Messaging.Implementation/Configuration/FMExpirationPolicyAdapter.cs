namespace CentralSystem.FlowManager.Messaging.Implementation.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Exceptions;
    using CentralSystem.Messaging;
    using CentralSystem.Messaging.FlowManager;

    /// <summary>
    /// Expiration policy adapter
    /// </summary>
    public sealed class FMExpirationPolicyAdapter
    {

        #region Properties

        /// <summary>
        /// Default time to be received time interval
        /// </summary>
        public TimeSpan DefaultTimeToBeReceived 
        { 
            get
            {
                return TimeSpan.FromMinutes(5);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Implementation of flow activities expiration policy.
        /// </summary>
        /// <param name="type">Type</param>
        public TimeSpan GetTimeToBeReceivedIntervalFor(System.Type type)
        {
            //Check expiration policy attribute only for activity commands and not activity result messages
            if (type.GetInterfaces().Contains(typeof(IFlowActivityCommand))
                && !type.GetInterfaces().Contains(typeof(IFlowActivityResultMessage)))
            {
                IEnumerable<FlowExpirationPolicyAttribute> flowExpirationPolicyAttributes = type.GetCustomAttributes(typeof(FlowExpirationPolicyAttribute), true)
                    .Select((attr) => (FlowExpirationPolicyAttribute)attr);

                //If message marked as canceled
                if (flowExpirationPolicyAttributes.FirstOrDefault(attr => attr.ExpirationPolicyType == FlowExpirationPolicyTypes.Canceled) != null)
                {
                    return TimeSpan.MaxValue;
                }

                return DefaultTimeToBeReceived;
            }

            return TimeSpan.MaxValue;
        }

        #endregion

    }
}
