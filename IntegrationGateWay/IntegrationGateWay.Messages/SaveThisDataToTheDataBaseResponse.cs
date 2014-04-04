namespace IntegrationGateWay.Messages
{
    using System;
    using NServiceBus;

    public class SaveThisDataToTheDataBaseResponse : IMessage
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        public string SomeData { get; set; }
    }
}