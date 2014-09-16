using NServiceBus;

namespace Ordering.Messages
{
    public interface IOrderPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int OrderId { get; set; }
    }
}