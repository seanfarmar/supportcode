namespace IntegrationGateWay.DataBaseSite
{
    using System;
    using Messages;
    using NServiceBus;

    public class SaveThisDataToTheDataBaseHandler : IHandleMessages<SaveThisDataToTheDataBase>
    {
        public IBus Bus { get; set; }

        public void Handle(SaveThisDataToTheDataBase message)
        {
            Console.WriteLine("Saving to the database origenal TransactionId: {0}", message.TransactionId);
            Console.WriteLine("reply over channel: {0}", message.GetHeader(Headers.OriginatingSite));
            // save the data to the database
            // Dipendency injection sample here: https://github.com/sfarmar/Samples/tree/master/DIDemo/

            //this shows how the gateway rewrites the return address to marshal replies to and from remote sites
            Bus.Reply<SaveThisDataToTheDataBaseResponse>(m =>
            {
                m.Id = Guid.NewGuid();
                m.TransactionId = message.TransactionId;
                m.SomeData = message.SomeData;
            });
        }
    }
}