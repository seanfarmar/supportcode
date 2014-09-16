namespace Ordering.Subscriber
{
    using System;
    using Messages;
    using NServiceBus;

    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>, IHandleMessages<PeopleSoftBackOrderMessage>,
        IHandleMessages<PeopleSoftCancellationMessage>,
        IHandleMessages<PeopleSoftShipmentNotificationMessage>, IHandleMessages<PeopleSoftResponseMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderPlaced message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message.OrderId);
        }

        public void Handle(PeopleSoftBackOrderMessage message)
        {
            Console.WriteLine(@"Handling: BackOrder messages for OrderID: {0}", message);
        }

        public void Handle(PeopleSoftCancellationMessage message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message);
        }

        public void Handle(PeopleSoftResponseMessage message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message);
        }

        public void Handle(PeopleSoftShipmentNotificationMessage message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message);
        }
    }
}