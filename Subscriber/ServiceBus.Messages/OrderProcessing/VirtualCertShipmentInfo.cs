using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class VirtualCertShipmentNotification
    {
        public string PurchaseOrderNumber { get; set; }

        [XmlElement(IsNullable = false)]
        public OrderLine OrderLineInformation { get; set; }

        [XmlElement(IsNullable = false)]
        public int SerialId { get; set; }

        [XmlElement(IsNullable = false)]
        public int QuantityShipped { get; set; }

        [XmlElement(IsNullable = false)]
        public int QuantityCanceled { get; set; }
    }
}