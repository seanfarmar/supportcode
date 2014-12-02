namespace WebSQP.Handlers
{
    using System;
    using NServiceBus;

    public class Message
	{

	}

    public class PlacementAddedToGripEventHandler : IHandleMessages<Message>
    {
        
        public void Handle(Message message)
        {
			throw new Exception();
        }
    }
}
