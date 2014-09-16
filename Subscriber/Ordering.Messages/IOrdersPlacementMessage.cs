namespace Ordering.Messages
{
    using NServiceBus;

    public interface IOrdersPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int[] OrderIds { get; set; }
    }
}