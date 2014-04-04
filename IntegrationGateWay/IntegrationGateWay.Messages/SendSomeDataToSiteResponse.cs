namespace IntegrationGateWay.Messages
{
    using System;
    using NServiceBus;

    public class SendSomeDataToSiteResponse : IMessage
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        public string Error { get; set; }
    }
}