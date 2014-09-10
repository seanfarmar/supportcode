namespace NServiceBusService.Handlers
{
    using System;

    using NServiceBus;

    public class MessageHandler : IHandleMessages<IMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(IMessage message)
        {
            Console.WriteLine("Handled.");
        }
    }
}