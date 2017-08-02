namespace NSBConcurrency.ComponentTwo
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Persistence.Sql;

    public class SagaTwoData : ContainSagaData
    {
        public virtual Guid IdTwo { get; set; }
    }

    [SqlSaga(nameof(SagaTwoData.IdTwo))]
    public class SagaTwo : Saga<SagaTwoData>
        , IAmStartedByMessages<MessageTwo>
    {
        public Task Handle(MessageTwo message, IMessageHandlerContext context)
        {
            return Task.FromResult(0);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaTwoData> mapper)
        {
            mapper.ConfigureMapping<MessageTwo>(m => m.IdTwo).ToSaga(s => s.IdTwo);
        }
    }
}