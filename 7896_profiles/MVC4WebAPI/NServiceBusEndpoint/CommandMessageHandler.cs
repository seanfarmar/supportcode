namespace NServiceBusEndpoint
{
    using System;
    using Messages;
    using NServiceBus;

    class CommandMessageHandler : IHandleMessages<CommandMessage>
    {
        public void Handle(CommandMessage message)
        {
            Console.WriteLine("Handeling CommandMessage with Id: {0} and Name: {1}", message.Id, message.Name);
        }
    }
}
