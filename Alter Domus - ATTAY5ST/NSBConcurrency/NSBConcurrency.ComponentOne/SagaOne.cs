namespace NSBConcurrency.ComponentOne
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Persistence.Sql;

    public class SagaOneData : ContainSagaData
    {
        public virtual Guid IdOne { get; set; }
    }

    [SqlSaga(nameof(SagaOneData.IdOne))]
    public class SagaOne : Saga<SagaOneData>
        , IAmStartedByMessages<MessageOne>
    {
        public Task Handle(MessageOne message, IMessageHandlerContext context)
        {
            return Task.FromResult(0);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaOneData> mapper)
        {
            mapper.ConfigureMapping<MessageOne>(m => m.IdOne).ToSaga(s => s.IdOne);
        }
    }
}