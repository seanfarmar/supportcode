namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using NServiceBus;

    public interface IOrdersPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int[] OrderIds { get; set; }
    }
}