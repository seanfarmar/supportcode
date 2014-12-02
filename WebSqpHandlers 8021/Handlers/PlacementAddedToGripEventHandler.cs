namespace WebSQP.Handlers
{
    using System;
    using Messages;
    using NServiceBus;

    public class PlacementAddedToGripEventHandler : IHandleMessages<Message>
    {
        
        public void Handle(Message message)
        {
			throw new Exception();
        }
    }
}
