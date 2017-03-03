using System;
using System.Threading.Tasks;
using NSBConcurrency.Messages;
using NServiceBus;

namespace NSBConcurrency.ComponentThree
{
    public class SagaThreeData : ContainSagaData
    {
        public virtual Guid IdThree { get; set; }
    }
    public class SagaThree : Saga<SagaThreeData>
        , IAmStartedByMessages<MessageThree>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaThreeData> mapper)
        {
            mapper.ConfigureMapping<MessageThree>(m => m.IdThree).ToSaga(s => s.IdThree);
        }

        public Task Handle(MessageThree message, IMessageHandlerContext context)
        {
            return Task.FromResult(0);
        }
    }
}
