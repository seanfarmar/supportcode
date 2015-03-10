namespace FinalSubscriber
{
    using System;
    using MyMessages;
    using NServiceBus;
    using NServiceBus.Saga;

    public class FinalMessageSaga : Saga<MySagaData>, IAmStartedByMessages<IMyEvent>
    {
        public void Handle(IMyEvent message)
        {
            Console.WriteLine("Final Subscriber received IEvent with Id {0}.", message.EventId);
            Console.WriteLine("Message time: {0}.", message.Time);
            Console.WriteLine("Message duration: {0}.", message.Duration);
            
            Data.EventId = message.EventId;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Saga Data IEvent Id {0}, type.", Data.EventId);        
            Console.ResetColor();
        
        
            Console.WriteLine("==========================================================================");
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<IMyEvent>(msg => msg.EventId).ToSaga(s => s.EventId);
        }
    }
}