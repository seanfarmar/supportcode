namespace Server.Saga
{
    using System;
    using System.Collections.Generic;
    using Messages;
    using Messgaes;
    using NServiceBus;
    using NServiceBus.Saga;

    public class TransactionSaga : Saga<TransactionSagaData>, IAmStartedByMessages<InternalTransactionMessage>, IHandleMessages<ReplayMessage>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<InternalTransactionMessage>(m => m.CorrelationId).ToSaga(m => m.CorrelationId);
        }

        public void Handle(InternalTransactionMessage message)
        {
            // save the saga data
            Data.CorrelationId = message.CorrelationId;

            Data.TransactionList.Add(message.TransactionId);

            Bus.Send<TransactionMessage>(m =>
            {
                m.CorrelationId = message.CorrelationId;
                m.TransactionId = message.TransactionId;
            });
        }

        public void Handle(ReplayMessage message)
        {
            throw new NotImplementedException();
        }
        
        public void Timeout(object state)
        {
            throw new NotImplementedException();
        }
    }

    public class ReplayMessageSagaFinder : IFindSagas<TransactionSagaData>.Using<ReplayMessage>
    {
          public ISagaPersister Persister { get; set; }

       public TransactionSagaData FindBy(ReplayMessage message)
        {
           var lookup = string.Format("{0}__{1}", message.TransactionId);

           return Persister.Get<TransactionSagaData>("SagaLookup", lookup);
       }
    }
    public class TransactionSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public List<int> TransactionList { get; set; }
    }
}