namespace NServiceBusService.Handlers
{
    using System;
    using NServiceBus;
    using NServiceBusMessages.Messages;

    class UnObtrusiveMessageHandler : IHandleMessages<UnObtrusiveMessage>
    {
        public void Handle(UnObtrusiveMessage message)
        {
           Console.WriteLine("Handeling Message with name: {0}, and id: {1}", message.Name, message.IdGuid);
        }
    }
}
