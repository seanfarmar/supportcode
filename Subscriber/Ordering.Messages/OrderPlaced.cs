namespace Ordering.Messages
{
    using System;
    using Hinda.Internal.ServiceBus.Messages.OrderProcessing;
    using NServiceBus;

    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
    }

    public class PeopleSoftBackOrderMessage : IEvent
    {
        private BackOrder[] BackOrders { get; set; }
    }

    public class PeopleSoftCancellationMessage : IEvent
    {
        private OrderCancellation[] Cancellations { get; set; }
    }

    public class PeopleSoftResponseMessage : IEvent
    {
        private PeopleSoftOrder Order { get; set; }
    }

    public class PeopleSoftShipmentNotificationMessage : IEvent
    {
        private ShipmentNotificationInfo ShipmentNotificationInformation { get; set; }
    }
}