namespace BmcHeadquarter.Handlers
{
    using System;
    using Headquarter.Messages;
    using NServiceBus;

    public class PriceUpdateReceivedMessageHandler : IHandleMessages<PriceUpdateReceived>
    {
        public void Handle(PriceUpdateReceived message)
        {
            //this shows how the gateway rewrites the return address to marshal replies to and from remote sites
            Console.WriteLine("Price update received by: " + message.BranchOffice);
        }
    }
}