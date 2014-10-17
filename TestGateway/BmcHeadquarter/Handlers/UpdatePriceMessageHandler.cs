namespace BmcHeadquarter.Handlers
{
    using System;
    using Headquarter.Messages;
    using NServiceBus;

    public class UpdatePriceMessageHandler : IHandleMessages<UpdatePrice>
    {
        public IBus Bus { get; set; }

        public void Handle(UpdatePrice message)
        {
            Console.WriteLine("Price update request received from the webclient, going to push it to the remote sites");
        }
    }
}