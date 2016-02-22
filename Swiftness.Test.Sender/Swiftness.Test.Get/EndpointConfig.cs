namespace Swiftness.Test.Get
{
    using System;
    using Common;
    using NServiceBus;
    using NServiceBus.Features;

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public EndpointConfig()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        public void Customize(BusConfiguration configuration)
        {
            //ConventionsBuilder conventions = configuration.Conventions();
            //conventions.DefiningEventsAs(t => typeof(IEvent).IsAssignableFrom(t) || t.Namespace != null && t.Namespace.StartsWith("ServiceControl.Contracts"));

            configuration.UsePersistence<NHibernatePersistence>();
            configuration.UseSerialization<JsonSerializer>();
            // configuration.Transactions().Enable();
            //configuration.Transactions().EnableDistributedTransactions();
            //configuration.DisableFeature<Audit>();

            //NServiceBus.Logging.LogManager.Use<Log4NetFactory>();

            configuration.UseContainer<StructureMapBuilder>();
            configuration.DisableFeature<Audit>();
            //configuration.Transactions().DisableDistributedTransactions();

            //Log4NetConfig logConfig = new Log4NetConfig();
            //logConfig.Start();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry(System.Diagnostics.Process.GetCurrentProcess().ProcessName, String.Format("Unhandled exception - {0}", (e.ExceptionObject as Exception).ToString()), System.Diagnostics.EventLogEntryType.Error);
        }
    }
}
