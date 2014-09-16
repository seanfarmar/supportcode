using Hinda.Internal.ServiceBus.Messages.OrderProcessing;
using NServiceBus;

namespace Ordering.Messages
{
    public interface IPeopleSoftShipmentNotificationMessage : IMessage
    {
        ShipmentNotificationInfo ShipmentNotificationInformation { get; set; }
    }
}
