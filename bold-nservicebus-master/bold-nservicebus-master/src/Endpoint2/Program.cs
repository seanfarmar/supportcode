using System;
using Bold.Infra.Messages;
using NServiceBus;

namespace Endpoint1
{
    class Program
    {

        static void Main()
        {
            Console.Title = "Samples.Azure.ServiceBus.Endpoint1";
            #region config

            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Samples.Azure.ServiceBus.Endpoint1");
            var scaleOut = busConfiguration.ScaleOut();
            scaleOut.UseSingleBrokerQueue();
            var transport = busConfiguration.UseTransport<AzureServiceBusTransport>();
            var asbConnectionString = ""; 
            if (string.IsNullOrWhiteSpace(asbConnectionString))
            {
                throw new Exception("asbConnectionString' missing.");
            }
            transport.ConnectionString(asbConnectionString);

            #endregion

            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();

            using (var bus = Bus.Create(busConfiguration))
            {
                Console.WriteLine("Ready to receive events");
                Console.WriteLine("Press ESC key to exit");
                while (true)
                {
                    var key = Console.ReadKey();

                    Console.WriteLine();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }

                }
            }
        }
    }
}