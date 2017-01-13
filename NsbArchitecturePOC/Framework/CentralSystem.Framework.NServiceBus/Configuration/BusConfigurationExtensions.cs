namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Configuration;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Diagnostics;
    using CentralSystem.Framework.Utils;
    using CustomPerformanceCounters = CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters;
    using global::NServiceBus.Config.ConfigurationSource;
    using global::NServiceBus.Features;
    using global::NServiceBus.Logging;
    using global::NServiceBus.Persistence;
    using global::NServiceBus.Settings;
    using CentralSystem.Framework.NServiceBus.Processing;

    /// <summary>
    /// Helper extension functions for standard configuration of NServiceBus in the NeoGames applications
    /// </summary>
    public static class BusConfigurationExtensions
    {

        #region Public Methods

        /// <summary>
        /// Set endpoint name
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="endpointName">Endpoint name</param>
        public static void SetEndpointName(this BusConfiguration busConfiguration, string endpointName)
        {
            NSBCommonSettings.EndpointName = endpointName;
            busConfiguration.EndpointName(NSBCommonSettings.EndpointName);
        }

        /// <summary>
        /// Set default configurations
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="disableCallbackReceiver">True - disable callback receiver. Has effect to creation of local endpoint</param>
        public static void SetDefaultServerConfiguration(this BusConfiguration busConfiguration, bool disableCallbackReceiver)
        {
            SetDefaultServerConfiguration(busConfiguration, new DefaultCustomConfigurationSource(), disableCallbackReceiver);
        }

        /// <summary>
        /// Set default server configurations
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="configurationSource">Configuration source</param>
        /// <param name="disableCallbackReceiver">True - disable callback receiver. Has effect to creation of local endpoint</param>
        public static void SetDefaultServerConfiguration(this BusConfiguration busConfiguration, IConfigurationSource configurationSource, bool disableCallbackReceiver)
        {
            //Don't crash process on any critical error
            busConfiguration.DefineCriticalErrorDefaultAction();

            //Register base units of work internal manager
            //IMPORTANT: Don't change dependency lifecycle setting (instance per transport message)
            busConfiguration.RegisterComponents(
                c => c.ConfigureComponent<CentralSystem.Framework.NServiceBus.Processing.BusTransportMessageProcessingManageUnitsOfWork>(
                    DependencyLifecycle.InstancePerUnitOfWork));

            //Register extended performance counters
            busConfiguration.ConfigureExtendedDefaultPerformanceCountersIfInstalled();

            busConfiguration.CustomConfigurationSource(configurationSource);

            busConfiguration.SetSqlTransport(disableCallbackReceiver);

            busConfiguration.EnableInstallers();
        }
 
        /// <summary>
        /// Default transport configuration
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="disableCallbackReceiver">True - disable callback receiver. Has effect to creation of local endpoint</param>
        /// <returns>Transport extensions</returns>
        public static TransportExtensions SetSqlTransport(this BusConfiguration busConfiguration, bool disableCallbackReceiver)
        {
            TransportExtensions<SqlServerTransport> sqlTransport = busConfiguration.UseTransport<SqlServerTransport>();
            if (disableCallbackReceiver)
            {
                sqlTransport.DisableCallbackReceiver();
            }
            return sqlTransport
                .ConnectionString(ConfigurationManager.ConnectionStrings[NSBCommonSettings.SQL_CONNECTION_STRING_NAME].ConnectionString);
        }

        /// <summary>
        /// Default persistence configuration according to requirement don't use DTC and reuse a same connection object
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="enableTransactionMode">Enable transaction mode</param>
        /// <returns>Transaction settings</returns>
        public static TransactionSettings SetNHibernetPersistence(this BusConfiguration busConfiguration, bool enableTransactionMode)
        {
            busConfiguration.UsePersistence<NHibernatePersistence>()
                .ConnectionString(ConfigurationManager.ConnectionStrings[NSBCommonSettings.SQL_CONNECTION_STRING_NAME].ConnectionString)
                .UseConfiguration(BuildNHibernateConfiguration())
                .RegisterManagedSessionInTheContainer();

            TransactionSettings transactionSettings = busConfiguration.Transactions();
            if (!enableTransactionMode)
            {
                transactionSettings.Disable();
            }
            else
            {
                //In this scenario to disable DTC to same database
                //the DAO service should receive SqlConnection object from NHibernet session context.
                //The created transaction is not relevant (POC results).
                transactionSettings.DisableDistributedTransactions();
                transactionSettings.DoNotWrapHandlersExecutionInATransactionScope();
            }

            //Disable all required persisting features by default
            //For enabling of feature - endpoint configuration class should configure explicitly
            busConfiguration.DisableFeature<global::NServiceBus.Features.TimeoutManager>();
            busConfiguration.DisableFeature<global::NServiceBus.Features.Sagas>();
            busConfiguration.DisableFeature<global::NServiceBus.Features.Outbox>();
            busConfiguration.DisableFeature<global::NServiceBus.Features.Scheduler>();
            busConfiguration.DisableFeature<global::NServiceBus.Features.NHibernateGatewayDeduplication>();
            busConfiguration.DisableFeature<global::NServiceBus.Features.NHibernateSubscriptionStorage>();

            //Register base units of work internal manager
            //IMPORTANT: Don't change dependency lifecycle setting (instance per transport message)
            busConfiguration.RegisterComponents(
                c => c.ConfigureComponent<BusTransportMessageProcessingManageUnitsOfWork>(
                    DependencyLifecycle.InstancePerUnitOfWork));

            //Register base units of work internal manager
            //IMPORTANT: Don't change dependency lifecycle setting (instance per transport message)
            busConfiguration.RegisterComponents(
                c => c.ConfigureComponent<BusExecutionContext>(
                    DependencyLifecycle.InstancePerUnitOfWork));

            return transactionSettings;
        }

        /// <summary>
        /// Configure second level retires mechanism
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="secondLevelRetryPolicy">Custom second level retry policy</param>
        public static void SetSecondLevelRetries(this BusConfiguration busConfiguration, BaseSecondLevelRetryPolicy secondLevelRetryPolicy)
        {
            busConfiguration.SecondLevelRetries().CustomRetryPolicy(secondLevelRetryPolicy.Execute);
        }

        /// <summary>
        /// Configure outbox feature if enabled configuration.
        /// According to NServiceBus documentation (http://docs.particular.net/nservicebus/outbox/)
        ///  to run Outbox feature required double configuration (code and application configuration file based configurations together).
        /// Current function allows to disable Outbox feature by configuration.
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        public static void ConfigureOutboxFeatureIfEnabled(this BusConfiguration busConfiguration)
        {
            bool isEnabledOutboxFeature = false;
            string isEnabledOutboxFeatureAsString = ConfigurationManager.AppSettings[NSBCommonSettings.NSB_OUTBOX_FEATURE_ENABLE_SETTING_NAME];
            if (isEnabledOutboxFeatureAsString != null && !Boolean.TryParse(isEnabledOutboxFeatureAsString, out isEnabledOutboxFeature))
            {
                throw new ConfigurationErrorsException(
                    string.Format("Invalid application setting value named '{0}'. Expected true/false value.", NSBCommonSettings.NSB_OUTBOX_FEATURE_ENABLE_SETTING_NAME));
            }

            if (isEnabledOutboxFeature)
            {
                //This feature allows to resolve duplicated processing of messages in the context of disabled DTC.
                busConfiguration.EnableFeature<NServiceBus.Features.Outbox>();

            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Default configuration of critical error handler
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        /// <param name="forceToCrashProcessOnUnknownCriticalError">True - force to crash process on unknown critical error</param>
        private static void DefineCriticalErrorDefaultAction(this BusConfiguration busConfiguration)
        {
            busConfiguration.TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages(NSBCommonSettings.DEFAULT_CRITICAL_ERROR_TIME_TO_WAIT);

            //Don't crash process on any critical fatal error
            busConfiguration.DefineCriticalErrorAction(
                new BusCriticalErrorDefaultAction(false).OnCriticalError);
        }

        /// <summary>
        /// Configure extended default performance counters in case 
        /// if installed NServiceBus category
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        private static void ConfigureExtendedDefaultPerformanceCountersIfInstalled(this BusConfiguration busConfiguration)
        {
            if (PerformanceCounterCategory.Exists(NSBCommonSettings.NSERVICEBUS_BUILTIN_PERFORMANCE_COUNTERS_CATEGORY_NAME))
            {
                busConfiguration.EnableCriticalTimePerformanceCounter();
                busConfiguration.ConfigureExtendedSLAPerformanceCounter();
            }
            if (PerformanceCounterCategory.Exists(NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINTS_PERFORMANCE_COUNTERS_CATEGORY_NAME))
            {
                //Initiate first initialization of custom performance counters
                CustomPerformanceCounters.BusCustomPerformanceCounters.InitializeCreators();

                //IMPORTANT: Don't change dependency lifecycle setting (instance per transport message)
                //Initialize all subscribers for performance counters activation
                busConfiguration.RegisterComponents(
                    c => c.ConfigureComponent<CustomPerformanceCounters.BusReceiveStartedEventSubscriber>(
                        DependencyLifecycle.InstancePerUnitOfWork));
                busConfiguration.RegisterComponents(
                    c => c.ConfigureComponent<CustomPerformanceCounters.BusTransportMessageProcessingSubscriber>(
                        DependencyLifecycle.InstancePerUnitOfWork));
            }
        }

        /// <summary>
        /// Build NHibernet configuration to connect to NServiceBus transaction
        /// </summary>
        /// <returns></returns>
        private static NHibernate.Cfg.Configuration BuildNHibernateConfiguration()
        {
            var configuration = new NHibernate.Cfg.Configuration()
                .SetProperties(new Dictionary<string, string>
                {
                    {
                        NHibernate.Cfg.Environment.ConnectionString,
                        ConfigurationManager.ConnectionStrings[NSBCommonSettings.SQL_CONNECTION_STRING_NAME].ConnectionString
                    },
                    {
                        NHibernate.Cfg.Environment.Dialect,
                        "NHibernate.Dialect.MsSql2012Dialect"
                    },
                });

            return configuration;
        }

        /// <summary>
        /// Configure SLA performance counter according to configuration
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        private static void ConfigureExtendedSLAPerformanceCounter(this BusConfiguration busConfiguration)
        {
            TimeSpan slaThreshold = NSBCommonSettings.MIN_AND_DEFAULT_ENDPOINT_SLA_THRESHOLD_TIME_INTERVAL;
            string slaThreasholdAsString = ConfigurationManager.AppSettings[NSBCommonSettings.NSB_SLA_THRESHOLD_APP_SETTING_NAME];
            if (slaThreasholdAsString != null && !TimeSpan.TryParse(slaThreasholdAsString, out slaThreshold))
            {
                throw new ConfigurationErrorsException(
                    string.Format("Invalid application setting value named '{0}'. Expected time interval format.", NSBCommonSettings.NSB_SLA_THRESHOLD_APP_SETTING_NAME));
            }
            busConfiguration.EnableSLAPerformanceCounter(slaThreshold);
        }

        #endregion

    }
}
