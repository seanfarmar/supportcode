namespace CentralSystem.FlowManager.Messaging.Implementation.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.Implementation.Configuration;
    using NServiceBus.Config;

    /// <summary>
    /// Flow manager NSB custom configuration source
    /// </summary>
    internal sealed class FMCustomConfigurationSource : BaseCustomConfigurationSource
    {

        #region Methods

        /// <summary>
        /// Override custom configuration
        /// </summary>
        /// <typeparam name="TConfigurationSectionType">Section configuration type</typeparam>
        /// <returns>Configuration section class</returns>
        public override TConfigurationSectionType GetConfiguration<TConfigurationSectionType>()
        {
            //Transport configuration scenario
            if (typeof(TConfigurationSectionType) == typeof(TransportConfig))
            {
                //Always expected created configuration settings from default source
                TransportConfig defaultTransportConfig = base.GetConfiguration<TConfigurationSectionType>() as TransportConfig;

                TransportConfig resultTransportConfig = new TransportConfig()
                    {
                        MaximumConcurrencyLevel = 100,
                        MaximumMessageThroughputPerSecond = 0,
                    };

                resultTransportConfig.MaxRetries = 10;

                return resultTransportConfig as TConfigurationSectionType;
            }
            else
            {
                return base.GetConfiguration<TConfigurationSectionType>();
            }
        }

        #endregion

    }
}
