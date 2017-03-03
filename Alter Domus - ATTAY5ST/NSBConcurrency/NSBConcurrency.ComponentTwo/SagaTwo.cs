using System;
using System.Threading.Tasks;
using NSBConcurrency.Messages;
using NServiceBus;

namespace NSBConcurrency.ComponentTwo
{
    public class SagaTwoData : ContainSagaData
    {
        public virtual Guid IdTwo { get; set; }
    }

    public class SagaTwo : Saga<SagaTwoData>
        , IAmStartedByMessages<MessageTwo>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaTwoData> mapper)
        {
            mapper.ConfigureMapping<MessageTwo>(m => m.IdTwo).ToSaga(s => s.IdTwo);
        }

        public Task Handle(MessageTwo message, IMessageHandlerContext context)
        {
            return Task.FromResult(0);
        }
    }
}
