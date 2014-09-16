using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IPeopleSoftShipmentNotificationMessage : IMessage
    {
        ShipmentNotificationInfo ShipmentNotificationInformation { get; set; }
    }
}
