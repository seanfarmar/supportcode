namespace FinalSubscriber
{
    using System;
    using MyMessages;
    using NServiceBus;

    class MyIEventHandler : IHandleMessages<IMyEvent>
    {
        public void Handle(IMyEvent message)
        {
            var mesageType = message.GetType();

            Console.WriteLine("In MyIEventHandler: FinalEventMessage IMyEvent with Id {0}. Message type: {1}", message.EventId, mesageType.FullName);
        }
    }
}