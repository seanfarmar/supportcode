using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    public interface IEpsPublishPriceListToGlobalShareMessage : IMessage
    {
        int BatchId { get; set; }
    }
}