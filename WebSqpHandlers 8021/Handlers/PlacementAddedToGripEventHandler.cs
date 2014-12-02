using System;
using System.Linq;
using System.Collections.Generic;
using NServiceBus;

namespace WebSqpHandlers.Handlers
{
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
