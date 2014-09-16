using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class OrderLineShipmentNotification
    {
        public string PurchaseOrderNumber { get; set; }

        [XmlElement(IsNullable = false)]
        public OrderLine OrderLineInformation { get; set; }

        public DateTime EstimatedShipmentDate { get; set; }

        [XmlElement(IsNullable = false)]
        public int QuantityShipped { get; set; }

        [XmlElement(IsNullable = false)]
        public int QuantityCanceled { get; set; }

        public string CarrierCode { get; set; }
        public string TrackingNumber { get; set; }
    }
}