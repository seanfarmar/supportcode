namespace IntegrationGateWay.GateWay
{
    using System;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;

    public class SaveThisDataToTheDataBaseSaga : Saga<SaveThisDataToTheDataBaseSagaData>, IHandleMessages<SendSomething>, IHandleMessages<SaveThisDataToTheDataBaseResponse>
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

            Bus.SendLocal(saveThisDataToTheDataBase);
        }

        public void Handle(SaveThisDataToTheDataBaseResponse message)
        {
            Console.WriteLine("SaveThisDataToTheDataBaseResponse handling incomming message with id: {0}", message.Id);

            // check if there are no error codes...

            MarkAsComplete();

        }
    }
}