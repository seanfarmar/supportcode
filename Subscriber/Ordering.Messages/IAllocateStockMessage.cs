using NServiceBus;

namespace Ordering.Messages
{
    public interface IAllocateStockMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int OrderId { get; set; }
    }
}