using System;
using DataSync.Common.AxisMessages.Contract;
using NServiceBus;

namespace DataSync.HQ.HQStoreMessageProcessor
{
    public class StoreOutboundMessageHandler : IHandleMessages<AxisStoreOutboundMessage>
    {
        public IBus Bus { get; set; }

        private AxisStoreOutboundMessage _message;

        public void Handle(AxisStoreOutboundMessage message)
        {
            _message = message;
            Console.WriteLine("Inside the StoreOutboundMessageHandler");
        }

      
    }
}