using System;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

class Program
{

    static void Main()
    {
        Console.Title = "Samples.Notifications";
        #region logging
        var defaultFactory = LogManager.Use<DefaultFactory>();
        defaultFactory.Level(LogLevel.Fatal);
        #endregion
        #region endpointConfig
        var busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.Notifications");
        #endregion
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.DisableFeature<SecondLevelRetries>();

        using (var bus = Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }

                var myMessage = new MyMessage {Id = Guid.NewGuid()};
                bus.SendLocal(myMessage);

                Console.WriteLine("Sent a new message with id: {0}", myMessage.Id.ToString("N"));
            }
        }
    }
}