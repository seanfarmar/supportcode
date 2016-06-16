using System;
using System.Threading.Tasks;
using CustomErrorHandelingBehavior;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

static class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        Console.Title = "Samples.ErrorHandling.WithoutSLR";
        LogManager.Use<DefaultFactory>()
            .Level(LogLevel.Warn);

        #region DisableSLR
        var endpointConfiguration = new EndpointConfiguration("Samples.ErrorHandling.WithoutSLR");
        endpointConfiguration.DisableFeature<SecondLevelRetries>();
        #endregion
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.SendFailedMessagesTo("error");

        // register the behavior in the pipline
        endpointConfiguration.Pipeline.Register("CustomExceptionHandelingBehaviorID", typeof(CustomExceptionHandelingBehavior), "CustomExceptionHandelingBehavior");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        try
        {
            Console.WriteLine("Press enter to send a message that will throw an exception.");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.O)
                {

                    var myMessage = new MyOkMessage
                    {
                        Id = Guid.NewGuid()
                    };
                    await endpointInstance.SendLocal(myMessage)
                        .ConfigureAwait(false);
                }

                if (key.Key == ConsoleKey.E)
                {
                    var myArgumentExceptionMessage = new MyArgumentExceptionMessage()
                    {
                        Id = Guid.NewGuid()
                    };
                    await endpointInstance.SendLocal(myArgumentExceptionMessage)
                        .ConfigureAwait(false);
                }

                if (key.Key == ConsoleKey.A)
                {
                    var myBusinessExceptionMessage = new MyBusinessExceptionMessage()
                    {
                        Id = Guid.NewGuid()
                    };
                    await endpointInstance.SendLocal(myBusinessExceptionMessage)
                        .ConfigureAwait(false);
                }
            }
        }
        finally
        {
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}