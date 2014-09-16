namespace Ordering.Server
{
    using NServiceBus;
    using NServiceBus.Features;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }

    public class Bootsrap : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Features.Disable<Sagas>();
            Configure.Features.Disable<TimeoutManager>();

            // we want to use sql for persistance so we follow : http://docs.particular.net/nservicebus/relational-persistence-using-nhibernate---nservicebus-4.x
            // i created a database called NSERVICEBUS manually

            Configure.Instance
                .UseNHibernateSubscriptionPersister(); // subscription storage using NHibernate



            // .UseNHibernateTimeoutPersister() // Timeout Persistance using NHibernate
            // .UseNHibernateSagaPersister(); // Saga Persistance using NHibernate
            // In case you are using Gateway and would like to configure the persistence, also do the below:
            //.UseNHibernateGatewayPersister(); // Gateway Persistance using NHibernate    
        }
    }
}