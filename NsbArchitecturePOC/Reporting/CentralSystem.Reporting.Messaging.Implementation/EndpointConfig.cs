namespace CentralSystem.Reporting.Messaging.Implementation
{
    using NServiceBus;
    using CentralSystem.Framework.NServiceBus.Configuration;
    using System;

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
        public void Customize(BusConfiguration configuration)
        {
            configuration.SetEndpointName("CSReportingPOC");

            configuration.SetDefaultServerConfiguration(true);

            configuration.SetNHibernetPersistence(false); //No transaction strategy

            configuration.SetDefaultServerCrossVerticalsConfiguration();

            configuration.RegisterComponents((reg) =>
            {
                //business
                RegisterReportingDependencies(reg);

                RegisterUploadDependencies(reg);

                RegisterEODFlowDependencies(reg);
            });
        }

        private void RegisterReportingDependencies(NServiceBus.ObjectBuilder.IConfigureComponents reg)
        {
            //reg.ConfigureComponent<IHourlyReportsActivity>((builder) => new HourlyReportsActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IEODReportsActivity>((builder) => new EODReportsActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IDrawBreakReportsActivity>((builder) => new DrawBreakReportsActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IHourlyValidationActivity>((builder) => new HourlySignedTicketsValidationActivity(), DependencyLifecycle.InstancePerCall);
        }

        private void RegisterUploadDependencies(NServiceBus.ObjectBuilder.IConfigureComponents reg)
        {
            //reg.ConfigureComponent<IUploadHourlyDrawSalesToSafeActivity>((builder) => new UploadHourlyDrawSalesToSafeActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadHourlyDrawSalesToLotteryActivity>((builder) => new UploadHourlyDrawSalesToLotteryActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadEODDrawSalesReportsToVendorActivity>((builder) => new UploadEODDrawSalesReportsToVendorActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadSalesReportsToVendorActivity>((builder) => new UploadSalesReportsToVendorActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadSalesTotalsToSafeActivity>((builder) => new UploadSalesTotalsToSafeActivity(), DependencyLifecycle.InstancePerCall);           
        }

        /// <summary>
        /// Register EOD flow dependencies
        /// </summary>
        /// <param name="reg"></param>
        private void RegisterEODFlowDependencies(NServiceBus.ObjectBuilder.IConfigureComponents reg)
        {
            //reg.ConfigureComponent<IGenerateICSCheckpointActivity>((builder) => new GenerateICSCheckpointActivity(), DependencyLifecycle.InstancePerCall);

            //reg.ConfigureComponent<IGenerateGameSalesReportActivity>((builder) => new GenerateGameSalesReportActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadGameSalesReportToVendorActivity>((builder) => new UploadGameSalesReportToVendorActivity(), DependencyLifecycle.InstancePerCall);

            //reg.ConfigureComponent<IGenerateGamePaidTicketsReportActivity>((builder) => new GenerateGamePaidTicketsReportActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadGamePaidTicketsReportToVendorActivity>((builder) => new UploadGamePaidTicketsReportToVendorActivity(), DependencyLifecycle.InstancePerCall);

            //reg.ConfigureComponent<IGenerateGameDrawsLiabilityReportActivity>((builder) => new GenerateGameDrawsLiabilityReportActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadGameDrawsLiabilityReportToVendorActivity>((builder) => new UploadGameDrawsLiabilityReportToVendorActivity(), DependencyLifecycle.InstancePerCall);

            //reg.ConfigureComponent<IGenerateGameFutureDrawsSalesReportActivity>((builder) => new GenerateGameFutureDrawsSalesReportActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadGameFutureDrawsSalesReportToVendorActivity>((builder) => new UploadGameFutureDrawsSalesReportToVendorActivity(), DependencyLifecycle.InstancePerCall);

            //reg.ConfigureComponent<IGenerateGameLiabilitybyDivisionReportActivity>((builder) => new GenerateGameLiabilitybyDivisionReportActivity(), DependencyLifecycle.InstancePerCall);
            //reg.ConfigureComponent<IUploadGameLiabilitybyDivisionReportToVendorActivity>((builder) => new UploadGameLiabilitybyDivisionReportToVendorActivity(), DependencyLifecycle.InstancePerCall);
        }

        #endregion
    }
}