using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSBConcurrency.Messages;
using NServiceBus;

namespace NSBConcurrency.ComponentOne
{
    public class SagaOneData : ContainSagaData
    {
        public virtual Guid IdOne { get; set; }
    }
    public class SagaOne : Saga<SagaOneData>
        , IAmStartedByMessages<MessageOne>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaOneData> mapper)
        {
            mapper.ConfigureMapping<MessageOne>(m => m.IdOne).ToSaga(s => s.IdOne);
        }

        public Task Handle(MessageOne message, IMessageHandlerContext context)
        {
            return Task.FromResult(0);
        }
    }
}
