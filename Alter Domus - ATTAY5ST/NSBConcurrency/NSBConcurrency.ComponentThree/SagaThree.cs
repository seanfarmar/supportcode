namespace NSBConcurrency.ComponentThree
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Persistence.Sql;

    public class SagaThreeData : ContainSagaData
    {
        public virtual Guid IdThree { get; set; }
    }

    [SqlSaga(nameof(SagaThreeData.IdThree))]
    public class SagaThree : Saga<SagaThreeData>
        , IAmStartedByMessages<MessageThree>
    {
        public Task Handle(MessageThree message, IMessageHandlerContext context)
        {
            return Task.FromResult(0);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaThreeData> mapper)
        {
            mapper.ConfigureMapping<MessageThree>(m => m.IdThree).ToSaga(s => s.IdThree);
        }
    }
}