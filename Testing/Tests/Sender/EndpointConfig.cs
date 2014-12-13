namespace Tests.Sender
{
    using Messages;
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            // NServiceBus provides the following durable storage options
            // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
            // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();
            
            // If you don't need a durable storage you can also use, configuration.UsePersistence<InMemoryPersistence>();
            // more details on persistence can be found here: http://docs.particular.net/nservicebus/persistence-in-nservicebus
            
            //Also note that you can mix and match storages to fit you specific needs. 
            //http://docs.particular.net/nservicebus/persistence-order
            configuration.UsePersistence<InMemoryPersistence>();

            configuration.Conventions().DefiningMessagesAs(m => m.Namespace != null && m.Namespace.Contains("Messages"));
        }
    }

    class Bootstrapper : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            var request = new AppTypeRequest { AppId = 111 };

            var appReceivedEvent = new SetAppType { AppId = 222 };

            Bus.SendLocal(appReceivedEvent);
            
            Bus.SendLocal(request);

        }

        public void Stop()
        {            
        }
    }
}
