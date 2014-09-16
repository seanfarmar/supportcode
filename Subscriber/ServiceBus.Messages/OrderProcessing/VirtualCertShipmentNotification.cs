using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class VirtualCertShipmentNotification : ShipmentNotificationBase
    {
        [XmlElement(IsNullable = false)]
        public DateTime ShipmentDate { get; set; }

        [XmlElement(IsNullable = false, ElementName = "SerialId")]
        public int[] SerialIdCollection { get; set; }
    }
}
