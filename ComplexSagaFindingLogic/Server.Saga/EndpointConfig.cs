namespace Server.Saga
{
    using NServiceBus;
    using NServiceBus.Persistence.Raven;
    using Raven.Client;
    using Raven.Client.Document;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
    }

    public class RavenBootstrapper : INeedInitialization
    {
        public void Init()
        {
            var store = new DocumentStore
            {
                Url = "http://localhost:8080",
                Conventions = { FindTypeTagName = RavenConventions.FindTypeTagName }
            };

            store.Initialize();
            
            Configure.Instance.Configurer.RegisterSingleton<IDocumentStore>(store);

            Configure.Instance.RavenPersistenceWithStore(store);

            Configure.Instance.Configurer.RegisterSingleton<ReplayMessageSagaFinder>(new ReplayMessageSagaFinder(store.OpenSession()));
        }
    }
}
