using System;
using DocumentServicesSaga.Messages.Commands;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace DocumentServicesSaga.StartupApp
{
    using Shared;

    class Program
    {
        static void Main(string[] args)
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("DocumentServicesSaga.StartupApp");
            busConfiguration.ApplyCommonConfiguration();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.Clear();
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

                    Console.WriteLine("Sending document extraction request at {0}.", DateTime.Now);
                    SendDocumentExtractionRequest(bus);
                }
            }
        }

        private static void SendDocumentExtractionRequest(IBus bus)
        {
            var cmd = new DocumentExtractionRequest
            {
                DocumentId = Guid.NewGuid(),
                ClientId = "CLIENT",
                FilePath = "c:\\temp\\docs\\doc.pdf"
            };

            bus.Send(cmd);
        }
    }
    class ConfigErrorQueue : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}
