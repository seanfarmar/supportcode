namespace DataSync.HQ.HQStoreMessageProcessor
{
    using System;
    using Common.AxisMessages.Contract;
    using NServiceBus;

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