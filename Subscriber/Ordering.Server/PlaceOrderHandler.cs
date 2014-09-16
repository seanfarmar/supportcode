namespace Ordering.Server
{
    using System;
    using Messages;
    using NServiceBus;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public IBus Bus { get; set; }

        public void Handle(PlaceOrder message)
        {
            Console.WriteLine(@"Order for Product:{0} placed with id: {1}", message.Product, message.Id);
            Console.WriteLine(@"Publishing: OrderPlaced for OrderID {1}", message.Product, message.Id);

            Bus.Publish<OrderPlaced>(e => { e.OrderId = message.Id; });
        }
    }
}