
using NServiceBus;

namespace DataSync.HQ.HQStoreMessageProcessor
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Worker
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UseTransport<AzureStorageQueueTransport>();
            configuration.UsePersistence<AzureStoragePersistence>();

            var conventions = configuration.Conventions();
            conventions.DefiningMessagesAs(t => t.Namespace.EndsWith("Contract"));
        }
    }
}