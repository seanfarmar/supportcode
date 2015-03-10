namespace FinalSubscriber
{
    using System;
    using NServiceBus;

    class Program
    {
        static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();
           
            busConfiguration.EndpointName("Sample.PubSub.FinalSubscriber");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.EnableInstallers();

            var startableBus = Bus.Create(busConfiguration);            
            
            using (startableBus.Start())
            {
                Console.WriteLine("To exit, Ctrl + C");
                Console.ReadLine();
            }
        }
    }
}
