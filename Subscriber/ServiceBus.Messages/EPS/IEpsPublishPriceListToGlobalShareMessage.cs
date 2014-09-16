namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    using NServiceBus;

    public interface IEpsPublishPriceListToGlobalShareMessage : IMessage
    {
        int BatchId { get; set; }
    }
}