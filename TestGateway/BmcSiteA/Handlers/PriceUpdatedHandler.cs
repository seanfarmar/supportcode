namespace BmcSiteA.Handlers
{
    using System;
    using Headquarter.Messages;
    using NServiceBus;

    public class PriceUpdatedHandler : IHandleMessages<UpdatePrice>
    {
        public IBus Bus { get; set; }

        public void Handle(UpdatePrice message)
        {
            Console.WriteLine("Price update request received");
        
            //this shows how the gateway rewrites the return address to marshal replies to and from remote sites
            Bus.Reply<PriceUpdateReceived>(m=>
                {
                    m.BranchOffice = "BmcSiteA";
                });
        }
    }
}
