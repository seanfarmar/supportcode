namespace IntegrationGateWay.GateWay
{
    using System;
    using Messages;
    using NServiceBus;

    public class SaveThisDataToTheDataBaseMessageHandler : IHandleMessages<SaveThisDataToTheDataBase>
    {
        public IBus Bus { get; set; }

        public void Handle(SaveThisDataToTheDataBase message)
        {
            Console.WriteLine("SendSomethingMessageHandler handling incomming message with id: {0}", message.Id);

            var sendSomeDataToSite = new SendSomeDataToSite
            {
                Id = Guid.NewGuid(),
                TransactionId = message.Id,
                SomeData = message.SomeData
            };

            Bus.SendToSites(new[] {"theDatabaseSiteKeyINCONFIG"}, sendSomeDataToSite);
        }
    }
}
