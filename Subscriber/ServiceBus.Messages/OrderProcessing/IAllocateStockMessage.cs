using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IAllocateStockMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int OrderId { get; set; }
    }
}