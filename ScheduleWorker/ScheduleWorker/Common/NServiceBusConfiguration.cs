namespace ScheduleWorker
{
    using NServiceBus;

    public class NServiceBusConfiguration : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
        }
    }
}