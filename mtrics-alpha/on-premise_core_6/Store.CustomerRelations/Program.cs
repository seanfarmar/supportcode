using System;
using System.Threading.Tasks;
using NServiceBus;
using Store.Messages.Events;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.Store.CustomerRelations";
        var endpointConfiguration = new EndpointConfiguration("Store.CustomerRelations");
        endpointConfiguration.ApplyCommonConfiguration(transport =>
        {
            transport.Routing().RegisterPublisher(typeof(OrderAccepted), "Store.Sales");
        });
        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}
