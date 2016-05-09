namespace DocumentServicesSaga.Shared
{
    using NServiceBus;

    public static class CommonConfiguration
    {
        public static void ApplyCommonConfiguration(this BusConfiguration busConfiguration)
        {
            busConfiguration.UseTransport<MsmqTransport>();
            //configuration.LicensePath("c:\\nservicebus\\license.xml");
            busConfiguration.UsePersistence<NHibernatePersistence>();
            busConfiguration.EnableInstallers();

            busConfiguration.Conventions()
                .DefiningCommandsAs(
                    t =>
                        t.Namespace != null && t.Namespace.StartsWith("DocumentServicesSaga.Messages") &&
                        t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(
                    t =>
                        t.Namespace != null && t.Namespace.StartsWith("DocumentServicesSaga.Messages") &&
                        t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(
                    t => t.Namespace != null && t.Namespace.StartsWith("DocumentServicesSaga.Messages"));
        }
    }
}