namespace Messages
{
    using System;
    using NServiceBus;

    public class CommandMessage : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
