using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Ordering.Messages;

namespace Ordering.Subscriber
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>, IHandleMessages<PeopleSoftBackOrderMessage>, IHandleMessages<PeopleSoftCancellationMessage>,
       IHandleMessages<PeopleSoftShipmentNotificationMessage>, IHandleMessages<PeopleSoftResponseMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(OrderPlaced message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message.OrderId);
         
        }
       
        public void Handle(PeopleSoftBackOrderMessage message)
        {
            Console.WriteLine(@"Handling: BackOrder messages for OrderID: {0}", message.ToString());

        }
        public void Handle(PeopleSoftCancellationMessage message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message.ToString());

        }

        public void Handle(PeopleSoftShipmentNotificationMessage message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message.ToString());

        }
        public void Handle(PeopleSoftResponseMessage message)
        {
            Console.WriteLine(@"Handling: OrderPlaced for OrderID: {0}", message.ToString());
        }
       
    }
}
