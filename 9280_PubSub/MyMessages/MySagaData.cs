namespace MyMessages
{
    using System;
    using NServiceBus.Saga;

    public class MySagaData : ContainSagaData
    {
        [Unique]
        public Guid EventId { get; set; }

        public string SomeData { get; set; }
    }
}
