using NServiceBus;

public static class CommonConfiguration
{
    public static void ApplyCommonConfiguration(this BusConfiguration configuration)
    {
        configuration.UseTransport<MsmqTransport>();
        configuration.LicensePath("c:\\nservicebus\\license.xml");
        configuration.UsePersistence<NHibernatePersistence>();
        configuration.EnableInstallers();

        configuration.Conventions()
            .DefiningCommandsAs(
                t =>
                    t.Namespace != null && t.Namespace.StartsWith("DocumentServicesSaga.Messages") &&
                    t.Namespace.EndsWith("Commands"))
            .DefiningEventsAs(
                t =>
                    t.Namespace != null && t.Namespace.StartsWith("DocumentServicesSaga.Messages") &&
                    t.Namespace.EndsWith("Events"));
    }
}