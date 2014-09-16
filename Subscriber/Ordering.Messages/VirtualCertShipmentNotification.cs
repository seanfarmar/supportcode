namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class VirtualCertShipmentNotification : ShipmentNotificationBase
    {
        [XmlElement(IsNullable = false)]
        public DateTime ShipmentDate { get; set; }

        [XmlElement(IsNullable = false, ElementName = "SerialId")]
        public int[] SerialIdCollection { get; set; }
    }
}