namespace Messages
{
    using System;
    using NServiceBus;

    public class CreateProductMessage : IMessage
    {
        public Guid GuidId { get; set; }

        public string Name { get; set; }
        public decimal ProductNumber { get; set; }
    }
}