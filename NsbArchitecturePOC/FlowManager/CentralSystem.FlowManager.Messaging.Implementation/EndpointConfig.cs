namespace CentralSystem.FlowManager.Messaging.Implementation
{
    using System;
    using System.Configuration;
    using CentralSystem.FlowManager.Messaging.Implementation.Configuration;
    using CentralSystem.FlowManager.Messaging.Implementation.Headers;
    using CentralSystem.Framework.NServiceBus.Configuration;
    using NServiceBus;
    using NServiceBus.Features;

    /// <summary>
    /// Use standard NServiceBus NeoGames strategy
    /// </summary>
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {

        #region IConfigureThisEndpoint Members

        /// <summary>
        /// Custom initialization
        /// </summary>
        /// <param name="configuration">Bus configuration</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "FlowManagerS")]
        public void Customize(BusConfiguration configuration)
        {
            try
            {
                configuration.SetEndpointName("FlowManagerPOC");

                configuration.SetDefaultServerConfiguration(new FMCustomConfigurationSource(),
                    false); //Don't disable receiver callback for the using in Force scenario

                configuration.SetNHibernetPersistence(true); //Transaction strategy

                configuration.SetDefaultServerCrossVerticalsConfiguration();

                configuration.Conventions().DefiningTimeToBeReceivedAs(
                    new FMExpirationPolicyAdapter().GetTimeToBeReceivedIntervalFor);

                configuration.SetSecondLevelRetries(new FMSecondLevelRetryPolicy());

                //To prevent timeout events receiving in the debug mode
                configuration.EnableFeature<TimeoutManager>();
                configuration.EnableFeature<Sagas>();

                //To resolve duplicated processing of messages in the context of disabled DTC
                configuration.ConfigureOutboxFeatureIfEnabled();

                configuration.RegisterComponents((reg) =>
                {
                    //Register internal messaging services
                    reg.ConfigureComponent<FMMessageBusHeadersInitializer>(DependencyLifecycle.InstancePerCall);
                });
            }
            catch (Exception ex)
            {
                //CentralLogger.Fatal(
                //    CommonMessages.INT_ERROR_START_APP_MSG, 
                //    CommonMessageCodes.INT_ERROR_START_APP_CODE, 
                //    ex);
                throw;
            }
        }

        #endregion

    }
}
