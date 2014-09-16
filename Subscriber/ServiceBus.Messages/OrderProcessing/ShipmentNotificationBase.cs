using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class ShipmentNotificationBase
    {
        [XmlElement(IsNullable = false)]
        public string ProductNumber { get; set; }

        [XmlElement(IsNullable = false)]
        public string OrderNumber { get; set; }

        [XmlElement(IsNullable = false)]
        public int OrderLineNumber { get; set; }

        [XmlElement(IsNullable = false)]
        public int QuantityShipped { get; set; }
    }
}