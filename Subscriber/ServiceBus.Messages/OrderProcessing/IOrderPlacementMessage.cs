using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IOrderPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int OrderId { get; set; }
    }
}