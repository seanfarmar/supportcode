
using System;
using DataSync.Store.StoreMessages.Contract.Messages;
using NServiceBus;

namespace DataSync.HQ.HQStoreMessageProcessor
{
    public class GiftCardPurchaseMessageHandler : IHandleMessages<GiftCardPurchase>
    {
        public IBus Bus { get; set; }

        public void Handle(GiftCardPurchase message)
        {
            Console.WriteLine("Save to HQ DB");
        }
    }
}