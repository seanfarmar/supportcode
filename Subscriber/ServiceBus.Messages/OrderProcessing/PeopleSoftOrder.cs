namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using System;

    [Serializable]
    public class PeopleSoftOrder
    {
        public string PONumber { get; set; }
        public OrderLine[] OrderLines { get; set; }
    }

    [Serializable]
    public class OrderLine
    {
        public int LineNumber { get; set; }
        public string OrderNumber { get; set; }
        public int POLineNumber { get; set; }
    }
}