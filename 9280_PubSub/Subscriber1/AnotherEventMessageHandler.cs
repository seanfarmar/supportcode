namespace Subscriber1
{
    using System;
    using MyMessages;
    using NServiceBus.Saga;

    public class AnotherEventMessageHandler : Saga<MySagaData>, IAmStartedByMessages<AnotherEventMessage>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<AnotherEventMessage>(m => m.EventId)
                .ToSaga(s => s.EventId);
        }

        public void Handle(AnotherEventMessage message)
        {
            Console.WriteLine("Subscriber 1 received AnotherEventMessage with Id {0}.", message.EventId);
            Console.WriteLine("Message time: {0}.", message.Time);
            Console.WriteLine("Message duration: {0}.", message.Duration);
            Console.WriteLine("==========================================================================");

            Data.EventId = message.EventId;
            Data.SomeData = "AnotherEventMessage";

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