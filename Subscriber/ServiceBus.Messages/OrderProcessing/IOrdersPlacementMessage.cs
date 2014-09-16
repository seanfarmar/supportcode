using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IOrdersPlacementMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int[] OrderIds { get; set; }
    }
}