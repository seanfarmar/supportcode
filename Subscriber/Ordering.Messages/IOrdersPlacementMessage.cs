using NServiceBus;

namespace Ordering.Messages
{
    public interface IOrdersPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int[] OrderIds { get; set; }
    }
}