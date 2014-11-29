
using NServiceBus;

namespace DataSync.HQ.HQStoreMessageProcessor
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Worker
    {
        static EndpointConfig()
        {

        }

        public void Customize(BusConfiguration builder)
        {
            builder.UseTransport<AzureStorageQueueTransport>();
            builder.UsePersistence<AzureStoragePersistence>();
        }
    }
}