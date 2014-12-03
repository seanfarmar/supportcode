using NServiceBus;

namespace DataSync.Store.StoreMessageProcessor
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseTransport<AzureStorageQueueTransport>();
           // ServiceBus = Bus.CreateSendOnly(configuration);

            var conventions = configuration.Conventions();
            conventions.DefiningMessagesAs(t => t.Namespace.EndsWith("Contract"));
        }
    }
}