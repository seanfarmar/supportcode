namespace NSBConcurrency
{
    using System.Data.SqlClient;
    using NServiceBus;
    using NServiceBus.Persistence.Sql;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            //TODO: NServiceBus provides multiple durable storage options, including SQL Server, RavenDB, and Azure Storage Persistence.
            // Refer to the documentation for more details on specific options.
            // endpointConfiguration.UsePersistence<SqlPersistence>();

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=NServiceBusPersistence;Integrated Security=True";
            persistence.SqlVariant(SqlVariant.MsSqlServer);
            persistence.ConnectionBuilder(() => { return new SqlConnection(connection); });

            // NServiceBus will move messages that fail repeatedly to a separate "error" queue. We recommend
            // that you start with a shared error queue for all your endpoints for easy integration with ServiceControl.
            endpointConfiguration.SendFailedMessagesTo("error");

            // NServiceBus will store a copy of each successfully process message in a separate "audit" queue. We recommend
            // that you start with a shared audit queue for all your endpoints for easy integration with ServiceControl.
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.UseSerialization<JsonSerializer>();

            endpointConfiguration.Conventions()
                .DefiningMessagesAs(t => t != null && t.Namespace != null && t.Namespace.Contains(".Messages"));
        }
    }
}