using System;
using DataSync.Common.AxisMessages.Contract;
using NServiceBus;

namespace DataSync.Store.StoreMessageProcessor
{
    public class AxisStoreOutboundMessageHandler : IHandleMessages<AxisStoreOutboundMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(AxisStoreOutboundMessage message)
        {
            Console.WriteLine("Processing message: " + Bus.CurrentMessageContext.Id);
        }
    }
}