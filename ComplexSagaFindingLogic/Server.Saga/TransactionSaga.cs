namespace Server.Saga
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Client.Messages;
    using Messages;
    using NServiceBus;
    using NServiceBus.Persistence.Raven;
    using NServiceBus.Saga;

    public class TransactionSaga : Saga<TransactionSagaData>, IAmStartedByMessages<InternalTransactionMessage>, IHandleMessages<ReplayMessage>, IHandleMessages<InternalTransactionControlMessage>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<InternalTransactionMessage>(m => m.CorrelationId).ToSaga(s => s.CorrelationId);
            ConfigureMapping<InternalTransactionControlMessage>(m => m.CorrelationId).ToSaga(s => s.CorrelationId);
        }

        public void Handle(InternalTransactionMessage message)
        {
            Console.WriteLine("Saga is handeling InternalTransactionMessage with sagaId: {0} TransactionId: {1} CorrelationId: {2}", Data.Id, message.TransactionId, message.CorrelationId);
            Console.WriteLine("======================================");
            
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
            Console.WriteLine("Saga is handeling ReplayMessage with TransactionId: {0}", message.TransactionId);
            Console.WriteLine("======================================");

            Data.TransactionRplyList.Add(message.TransactionId);

            TryCompleteSaga();
        }

        public void Handle(InternalTransactionControlMessage message)
        {
            Console.WriteLine("Saga is handeling InternalTransactionControlMessage with CorrelationId: {0}", message.CorrelationId);

            Data.TransactionControlList = message.TransactionList;

            TryCompleteSaga();
        }        
        
        private void TryCompleteSaga()
        {
            if (Data.TransactionControlList == null)
            {
                Console.WriteLine("TryCompleteSaga: Data.TransactionControlList == null, with sagaId: {0}", Data.Id);
                return;
            }

            if (Data.TransactionControlList.Count != Data.TransactionList.Count)
            {
                Console.WriteLine("TryCompleteSaga: Data.TransactionControlList.Count != Data.TransactionList.Count, with sagaId: {0}", Data.Id);
                return;
            }


            if(Data.TransactionControlList.Count != Data.TransactionRplyList.Count)
            {
                Console.WriteLine("TryCompleteSaga: Data.TransactionControlList.Count != Data.TransactionRplyList.Count, with sagaId: {0}", Data.Id);
                return;
            }

            
            bool areEqual = Data.TransactionList.OrderBy(x => x).SequenceEqual(Data.TransactionRplyList.OrderBy(x => x));

            if (!areEqual)
            {
                Console.WriteLine("TryCompleteSaga: !areEqual, with sagaId: {0}", Data.Id);
                return;
            }

            Console.WriteLine("Saga complete, with sagaId: {0}", Data.Id);

            MarkAsComplete();
        }
    }

    public class ReplayMessageSagaFinder : IFindSagas<TransactionSagaData>.Using<ReplayMessage>
    {
        public RavenSessionFactory RavenSessionFactory { get; set; }
        
        public TransactionSagaData FindBy(ReplayMessage message)
        {
            var saga = RavenSessionFactory.Session.Query<TransactionSagaData>()
            .FirstOrDefault(t => t.TransactionList.Any(x => x == message.TransactionId));

            if(saga == null) throw new Exception("can't find saga probably due to Raven's eventual consistancy, index is not found");

            return saga;
        }
    }

    public class TransactionSagaData : IContainSagaData
    {
        public TransactionSagaData()
        {
            TransactionRplyList = new List<Guid>();
            TransactionList = new List<Guid>();
        }

        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        [Unique]
        public Guid CorrelationId { get; set; }
        public List<Guid> TransactionList { get; set; }
        public List<Guid> TransactionRplyList { get; set; }
        public List<Guid> TransactionControlList { get; set; }
   }
}