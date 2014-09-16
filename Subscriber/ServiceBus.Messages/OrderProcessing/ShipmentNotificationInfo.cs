using System;
using System.Xml.Serialization;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    [Serializable]
    public class ShipmentNotificationInfo
    {
        [XmlElement(IsNullable = false)]
        public OrderLineShipmentNotification[] OrderLineShipmentNotifications { get; set; }

        [XmlElement(IsNullable = false)]
        public VirtualCertShipmentNotification[] VirtualCertShipmentNotifications { get; set; }
    }
}
