using System;
using System.Data.SqlClient;
using NServiceBus;
using NServiceBus.Encryption.MessageProperty;
using NServiceBus.MessageMutator;
using NServiceBus.Persistence.Sql;
using System.Text;

public static class CommonConfiguration
{
    public static void ApplyCommonConfiguration(this EndpointConfiguration endpointConfiguration,
        Action<TransportExtensions<MsmqTransport>> messageEndpointMappings = null)
    {
        var transport = endpointConfiguration.UseTransport<MsmqTransport>();
        messageEndpointMappings?.Invoke(transport);

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        var connection = @"Data Source=.\SqlExpress;Initial Catalog=NServiceBusStore;Integrated Security=True";
        persistence.SqlVariant(SqlVariant.MsSqlServer);
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(connection);
            });

        var subscriptions = persistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));

        var defaultKey = "2015-10";
        var ascii = Encoding.ASCII;
        var encryptionService = new RijndaelEncryptionService(
            encryptionKeyIdentifier: defaultKey,
            key: ascii.GetBytes("gdDbqRpqdRbTs3mhdZh9qCaDaxJXl+e6"));
        endpointConfiguration.EnableMessagePropertyEncryption(encryptionService);
        endpointConfiguration.RegisterMessageMutator(new DebugFlagMutator());

#pragma warning disable 618
        endpointConfiguration.EnableMetrics().SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromSeconds(1));
#pragma warning restore 618

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        
        endpointConfiguration.HeartbeatPlugin(
            serviceControlQueue: "Particular.ServiceControl");

        endpointConfiguration.SagaPlugin(
            serviceControlQueue: "Particular.ServiceControl");
    }
}