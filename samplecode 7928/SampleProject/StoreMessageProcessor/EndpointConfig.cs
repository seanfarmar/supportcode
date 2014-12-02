using NServiceBus;

namespace DataSync.Store.StoreMessageProcessor
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        private ISendOnlyBus ServiceBus { get; set; }

        static EndpointConfig()
        {
   
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseTransport<AzureStorageQueueTransport>();
           // ServiceBus = Bus.CreateSendOnly(configuration);
          
        }
    }
}