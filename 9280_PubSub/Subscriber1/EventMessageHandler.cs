namespace Subscriber1
{
    using System;
    using MyMessages;
    using NServiceBus.Saga;

    public class EventMessageHandler : Saga<MySagaData>, IAmStartedByMessages<EventMessage>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<EventMessage>(m => m.EventId)
                .ToSaga(s => s.EventId);
        }

        public void Handle(EventMessage message)
        {
            Console.WriteLine("Subscriber 1 received EventMessage with Id {0}.", message.EventId);
            Console.WriteLine("Message time: {0}.", message.Time);
            Console.WriteLine("Message duration: {0}.", message.Duration);
            Console.WriteLine("==========================================================================");

            Data.EventId = message.EventId;
            Data.SomeData = "EventMessage";

            var eventMessage = new FinalEventMessage
            {
                EventId = message.EventId,
                Time = DateTime.Now.Second > 30 ? (DateTime?)DateTime.Now : null,
                Duration = TimeSpan.FromSeconds(99999D)
            };

            Bus.Publish(eventMessage);
        }
    }
}