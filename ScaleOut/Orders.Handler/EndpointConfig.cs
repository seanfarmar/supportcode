using NServiceBus;

namespace Orders.Handler
{
    using System.Diagnostics;
    using NServiceBus.Persistence;

    class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            // For production use, please select a durable persistence.
            // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
            // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();
            //if (Debugger.IsAttached)
            //{
            //    configuration.UsePersistence<InMemoryPersistence>();
            //}

            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith("Messages"));

            configuration.UsePersistence<RavenDBPersistence>();

        }
    }
    
    class ConfiguringTheDistributorWithTheFluentApi : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            // uncomment one of the following lines if you want to use the fluent api instead. Remember to 
            // remove the "Master" profile from the command line Properties->Debug
            // configuration.RunMSMQDistributor();

            // or if you want to run the distributor only and no worker
            // configuration.RunMSMQDistributor(false);

            // or if you want to be a worker, remove the "Worker" profile from the command line Properties -> Debug
            // configuration.EnlistWithMSMQDistributor(); 
        }
    }
}
