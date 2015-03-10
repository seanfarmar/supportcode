namespace FinalSubscriber
{
    using System;
    using MyMessages;
    using NServiceBus;

    class FinalEventMessageHandler : IHandleMessages<FinalEventMessage>
    {
        public void Handle(FinalEventMessage message)
        {
            Console.WriteLine("In FinalEventMessageHandler: FinalEventMessage with Id {0}.", message.EventId);
        }
    }
}
