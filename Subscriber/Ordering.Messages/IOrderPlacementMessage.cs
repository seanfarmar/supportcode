namespace Ordering.Messages
{
    using NServiceBus;

    public interface IOrderPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int OrderId { get; set; }
    }
}