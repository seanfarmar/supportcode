namespace SelfHosingConsoleHost
{
    using System;
    using NServiceBus;

    public class TestMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}