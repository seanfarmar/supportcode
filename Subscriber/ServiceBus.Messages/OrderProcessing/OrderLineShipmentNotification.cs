using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class OrderLineShipmentNotification : ShipmentNotificationBase
    {
        [XmlElement(IsNullable = true)]
        public int? SequenceNumber { get; set; }

        [XmlElement(IsNullable = false)]
        public DateTime ShipmentDate { get; set; }

        [XmlElement(IsNullable = false)]
        public string CarrierCode { get; set; }

        [XmlElement(IsNullable = false, ElementName = "TrackingNumber")]
        public string[] TrackingNumbers { get; set; }
    }
}
