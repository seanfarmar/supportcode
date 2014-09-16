namespace Ordering.Messages
{
    using System;
    using System.Xml.Serialization;
    using Hinda.Internal.ServiceBus.Messages.OrderProcessing;

    [Serializable]
    public class BackOrderCollection
    {
        public BackOrder[] BackOrders { get; set; }
    }

    [Serializable]
    public class BackOrder : ShipmentNotificationBase
    {
        [XmlElement(IsNullable = false)]
        public DateTime EstimatedShipmentDate { get; set; }

        [XmlElement(IsNullable = false)]
        public int QuantityBackOrdered { get; set; }
    }
}