namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ShipmentNotificationInfo
    {
        [XmlElement(IsNullable = false)]
        public OrderLineShipmentNotification[] OrderLineShipmentNotifications { get; set; }

        [XmlElement(IsNullable = false)]
        public VirtualCertShipmentNotification[] VirtualCertShipmentNotifications { get; set; }
    }
}