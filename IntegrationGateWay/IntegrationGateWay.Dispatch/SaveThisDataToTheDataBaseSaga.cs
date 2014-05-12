namespace IntegrationGateWay.Dispatch
{
    using System;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;

    public class SaveThisDataToTheDataBaseSaga : Saga<SaveThisDataToTheDataBaseSagaData>, IAmStartedByMessages<SendSomething>, IHandleMessages<SaveThisDataToTheDataBaseResponse>
    {
        public IBus Bus { get; set; }

        public void Handle(SendSomething message)
        {
            Console.WriteLine("SaveThisDataToTheDataBase handling incomming message with id: {0}", message.Id);

            var saveThisDataToTheDataBase = new SaveThisDataToTheDataBase
            {
                Id = Guid.NewGuid(),
                TransactionId = message.Id,
                SomeData = message.SomeData
            };

            Console.WriteLine("Sending to database site message with TransactionId: {0}", saveThisDataToTheDataBase.TransactionId);

            Bus.SendToSites(new[] { "DataBaseSite" }, saveThisDataToTheDataBase);
        }

        public void Handle(SaveThisDataToTheDataBaseResponse message)
        {
            Console.WriteLine("SaveThisDataToTheDataBaseResponse handling replay message with transactionId: {0}", message.TransactionId);

            // check if there are no error codes...

            // retry logic and so on, no need to try catch as this is done by the framework...

            MarkAsComplete();
        }
    }
}