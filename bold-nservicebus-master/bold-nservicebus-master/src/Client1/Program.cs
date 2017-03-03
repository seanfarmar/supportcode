using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bold.Infra.Messages;
using NServiceBus;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "Samples.Azure.ServiceBus.Client1";
            #region config

            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Samples.Azure.ServiceBus.Client1");
            var scaleOut = busConfiguration.ScaleOut();
            scaleOut.UseSingleBrokerQueue();
            var transport = busConfiguration.UseTransport<AzureServiceBusTransport>();
            var asbConnectionString = "";
            if (string.IsNullOrWhiteSpace(asbConnectionString))
            {
                throw new Exception("asbConnectionString is missing");
            }
            transport.ConnectionString(asbConnectionString);

            #endregion

            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press 'enter' to send a message");
                Console.WriteLine("Press ESC key to exit");

                while (true)
                {
                    var key = Console.ReadKey();
                    
                    Console.WriteLine();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        return;
                    }

                    var orderId = Guid.NewGuid();
                    var message = new ShipOrderCommand
                    {
                        OrderId = orderId,
                        ShippingDate = DateTime.UtcNow,
                    };
                    bus.SetMessageHeader(message, BoldMessageHeaders.ContentId, orderId.ToString());
                    bus.SetMessageHeader(message, BoldMessageHeaders.ContentVersion, DateTime.UtcNow.Ticks.ToString());
                    bus.Send("Samples.Azure.ServiceBus.Endpoint2", message);
                    Console.WriteLine("ShipOrderCommand sent");
                }
            }
        }
    }
}
