using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class OrderCancellationCollection
    {
        public OrderCancellation[] Cancellations { get; set; }
    }

    [Serializable]
    public class OrderCancellation : ShipmentNotificationBase
    {
        [XmlElement(IsNullable = false)]
        public int QuantityCanceled { get; set; }
    }
}
