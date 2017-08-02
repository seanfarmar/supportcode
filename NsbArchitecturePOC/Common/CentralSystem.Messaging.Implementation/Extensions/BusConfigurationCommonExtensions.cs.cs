namespace NServiceBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging;
    using CentralSystem.Messaging.FlowManager;
    using CentralSystem.Messaging.Implementation.Configuration;
    using CentralSystem.Messaging.Implementation.Handlers;
    using CentralSystem.Messaging.Implementation.Headers;
    using CentralSystem.Messaging.Implementation.Headers.FlowManager;

    /// <summary>
    /// Application level common Bus initialization api
    /// </summary>
    public static class BusConfigurationCommonExtensions
    {

        #region Public Methods

        /// <summary>
        /// Set default server cross verticals configuration
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        public static void SetDefaultServerCrossVerticalsConfiguration(this BusConfiguration busConfiguration)
        {
            busConfiguration.SetDefaultCrossVerticalsLoggerConfiguration();
            busConfiguration.SetDefaultCrossVerticalsHeadersInitializationConfiguration();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Set default cross verticals logger configuration
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        private static void SetDefaultCrossVerticalsLoggerConfiguration(this BusConfiguration busConfiguration)
        {
            busConfiguration.RegisterComponents((reg) =>
            {
                //reg.ConfigureComponent<BusCentralLoggerAdapter>(DependencyLifecycle.InstancePerCall);
            });
        }

        /// <summary>
        /// Set default cross verticals headers initialization configuration
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        public static void SetDefaultCrossVerticalsHeadersInitializationConfiguration(this BusConfiguration busConfiguration)
        {
            busConfiguration.RegisterComponents((reg) =>
            {
                reg.ConfigureComponent<BaseFlowInstanceBusHeadersInitializer>(DependencyLifecycle.InstancePerCall);
                reg.ConfigureComponent<BaseFlowStepInstanceBusHeadersInitializer>(DependencyLifecycle.InstancePerCall);
            });
        }

        #endregion

    }
}
