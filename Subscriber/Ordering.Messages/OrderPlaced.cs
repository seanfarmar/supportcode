using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinda.Internal.ServiceBus.Messages.OrderProcessing;
using NServiceBus;

namespace Ordering.Messages
{
    public class OrderPlaced : IEvent
    {
        public Guid OrderId { get; set; }
    }

    public class PeopleSoftBackOrderMessage : IEvent
    {
        BackOrder[] BackOrders { get; set; }
    }

    public class PeopleSoftCancellationMessage : IEvent
    {
        OrderCancellation[] Cancellations { get; set; }
    }

    public class PeopleSoftResponseMessage : IEvent
    {
        PeopleSoftOrder Order { get; set; }
    }

    public class PeopleSoftShipmentNotificationMessage : IEvent
    {
        ShipmentNotificationInfo ShipmentNotificationInformation { get; set; }
    }
   
}
