using Bold.Infra.NServiceBus.InboxOutbox.InboxFeature;
using Microsoft.WindowsAzure;
using NServiceBus;
using NServiceBus.Persistence;
using NServiceBus.Persistence.NHibernate;

namespace Bold.Infra.NServiceBus.InboxOutbox
{
    public static class BusConfigurationExtensions
    {
        public static void EnableInboxAndOutbox(this BusConfiguration configuration)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Bold.DatabaseConnectionString");
            var schemaName = CloudConfigurationManager.GetSetting("NServiceBus/Persistence/NHibernate/default_schema");

            var inboxSettings = new InboxSettings(schemaName);
            var startupDbContext = new InboxStartupDbContext(connectionString, inboxSettings);
            startupDbContext.InitializeDatabase();

            configuration.EnableFeature<Inbox>();
            configuration.EnableFeature<InboxStartup>();

            //Todo: This code will be initialiezed by endpoint configuration
            //configuration.UsePersistence<NHibernatePersistence>()
            //             .ConnectionString(connectionString)
            //             .RegisterManagedSessionInTheContainer();

            configuration.EnableOutbox();

            configuration.RegisterComponents(r => r.ConfigureComponent(b => inboxSettings, DependencyLifecycle.InstancePerUnitOfWork));
            configuration.RegisterComponents(r => r.ConfigureComponent(b => b.Build<NHibernateStorageContext>().Connection, DependencyLifecycle.InstancePerUnitOfWork));
        }

        public static void EnableInbox(this BusConfiguration configuration)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Bold.DatabaseConnectionString");
            var schemaName = CloudConfigurationManager.GetSetting("NServiceBus/Persistence/NHibernate/default_schema");

            var inboxSettings = new InboxSettings(schemaName);
            var startupDbContext = new InboxStartupDbContext(connectionString, inboxSettings);
            startupDbContext.InitializeDatabase();

            configuration.EnableFeature<Inbox>();
            configuration.EnableFeature<InboxStartup>();

            configuration.UsePersistence<NHibernatePersistence>()
                .ConnectionString(connectionString)
                .RegisterManagedSessionInTheContainer();

            configuration.RegisterComponents(r => r.ConfigureComponent(b => inboxSettings, DependencyLifecycle.InstancePerUnitOfWork));
            configuration.RegisterComponents(r => r.ConfigureComponent(b => b.Build<NHibernateStorageContext>().Connection, DependencyLifecycle.InstancePerUnitOfWork));
        }
    }
}
