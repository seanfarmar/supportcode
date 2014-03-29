namespace Tests
{
    using System;
    using System.Collections.Generic;
    using Client.Messages;
    using NUnit.Framework;
    using Raven.Client;
    using Raven.Client.Embedded;
    using Server.Saga;

    [TestFixture]
    public class SagaFiderTests
    {
        private EmbeddableDocumentStore _store;
        private IDocumentSession _session;

        [SetUp]
        public void SetUp()
        {
            _store = new EmbeddableDocumentStore {RunInMemory = true};

            _store.Initialize();
        }

        [Test]
        public void test_sagaFinder_query()
        {
            var replyMessage = CreateTestData();

            using (var session = _store.OpenSession())
            {
                var sutSagaFinder = new ReplayMessageSagaFinder(session);

                var found = sutSagaFinder.FindBy(replyMessage);

                Assert.IsInstanceOf(typeof (TransactionSagaData), found, "Could not find saga...");

                Assert.AreEqual(found.Id, replyMessage.Id, "Wrong saga found");

                Assert.IsTrue(found.TransactionRplyList.Contains(replyMessage.TransactionId), "Can't find transactionId in selseted saga");
            }
        }

        private ReplayMessage CreateTestData()
        {
            Guid messageTransactionId = Guid.NewGuid();;
            Guid messageId = Guid.NewGuid();
            
            for (int i = 0; i < 5; i++)
            {
                var sagaData = new TransactionSagaData();
                var sagaId = Guid.NewGuid();
                var t1 = Guid.NewGuid();
                var t2 = Guid.NewGuid();
                var t3 = Guid.NewGuid();
                var t4 = Guid.NewGuid();
                
                var transactionList = new List<Guid> { t1, t2, t3, t4 };

                sagaData.Id = sagaId;
                sagaData.TransactionControlList = transactionList;
                sagaData.TransactionRplyList = transactionList;
                sagaData.TransactionList = transactionList;
                
                using (var session = _store.OpenSession())
                {
                    session.Store(sagaData);

                    session.SaveChanges();
                }

                if (i != 3) continue;
                messageTransactionId = t3;
                messageId = sagaId;
            }

            return new ReplayMessage { Id = messageId, TransactionId = messageTransactionId }; ;
        }
    }
}
