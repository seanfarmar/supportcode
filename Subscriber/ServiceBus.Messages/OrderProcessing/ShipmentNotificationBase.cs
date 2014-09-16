namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using System;
    using System.Xml.Serialization;

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