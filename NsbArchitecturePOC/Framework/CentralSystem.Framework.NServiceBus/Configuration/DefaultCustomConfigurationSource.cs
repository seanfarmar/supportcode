namespace CentralSystem.Framework.NServiceBus.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.Exceptions;
    using CentralSystem.Framework.NServiceBus.Consts;
    using global::NServiceBus.Config;
    using global::NServiceBus.Config.ConfigurationSource;

    /// <summary>
    /// Default custom configuration source.
    /// Provide override the basic method for specific vertical
    /// </summary>
    public class DefaultCustomConfigurationSource : IConfigurationSource
    {

        #region IConfigurationSource Members

        /// <summary>
        /// Return configuration source
        /// </summary>
        /// <typeparam name="TConfigurationSectionType">Section configuration type</typeparam>
        /// <returns>Configuration section class</returns>
        public virtual TConfigurationSectionType GetConfiguration<TConfigurationSectionType>() where TConfigurationSectionType : class, new()
        {
            TConfigurationSectionType resultConfig = ConfigurationManager.GetSection(typeof(TConfigurationSectionType).Name) as TConfigurationSectionType;

            //Transport configuration override scenario
            if (typeof(TConfigurationSectionType) == typeof(TransportConfig))
            {
                TransportConfig defaultTransportConfig = resultConfig as TransportConfig;

                if (resultConfig == null)
                {
                    defaultTransportConfig = new TransportConfig
                    {
                        MaximumConcurrencyLevel = NSBCommonSettings.DEFAULT_MAXIMUM_CONCURRENCY_LEVEL,
                        MaxRetries = NSBCommonSettings.DEFAULT_MAX_RETRIES,
                        MaximumMessageThroughputPerSecond = NSBCommonSettings.DEFAULT_MAXIMUM_MESSAGE_THROUGHPUT_PER_SECOND,
                    };
                }

                TransportConfig resultTransportConfig = new TransportConfig()
                {
                    MaximumConcurrencyLevel = defaultTransportConfig.MaximumConcurrencyLevel,
                    MaxRetries = defaultTransportConfig.MaxRetries,
                    MaximumMessageThroughputPerSecond = defaultTransportConfig.MaximumMessageThroughputPerSecond,
                };

                int maximumConcurrencyLevel;
                if (TryFindAndParsePositiveAppSetting(NSBCommonSettings.MAXIMUM_CONCURRENCY_LEVELPARAM_APP_SETTING_NAME, out maximumConcurrencyLevel))
                {
                    resultTransportConfig.MaximumConcurrencyLevel = maximumConcurrencyLevel;
                }

                return resultTransportConfig as TConfigurationSectionType;
            }
            else if (resultConfig == null) // If not exist override configuration in the file - return default
            {
                if (typeof(TConfigurationSectionType) == typeof(MessageForwardingInCaseOfFaultConfig))
                {
                    return new MessageForwardingInCaseOfFaultConfig
                    {
                        ErrorQueue = NSBCommonSettings.DEFAULT_ERROR_QUEUE_NAME,
                    } as TConfigurationSectionType;
                }
            }

            return resultConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Try parse if exists parameter
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="result">Result value</param>
        /// <returns>True - setting found</returns>
        private bool TryFindAndParsePositiveAppSetting(string name, out int result)
        {
            result = 0;
            string appSettingValue = ConfigurationManager.AppSettings[name];

            if (string.IsNullOrEmpty(appSettingValue))
            {
                return false;
            }

            if (!System.Int32.TryParse(appSettingValue, out result))
            {
                throw new CentralSystemException("Invalid configuration parameter value named " + name);
            }

            if (result < 1)
            {
                throw new CentralSystemException("Expected positive app setting from configuration parameter value named " + name);
            }

            return true;
        }

        #endregion

    }
}
