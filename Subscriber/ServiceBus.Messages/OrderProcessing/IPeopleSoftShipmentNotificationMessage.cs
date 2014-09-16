namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using NServiceBus;

    public interface IPeopleSoftShipmentNotificationMessage : IMessage
    {
        ShipmentNotificationInfo ShipmentNotificationInformation { get; set; }
    }
}