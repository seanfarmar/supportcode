namespace IntegrationGateWay.Messages
{
    using System;
    using NServiceBus;

    public class SendSomething : IMessage
    {
        public Guid Id { get; set; }

        public string SomeData { get; set; }
    }
}
