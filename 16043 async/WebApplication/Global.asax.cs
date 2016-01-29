using System;
using System.Web;
using NServiceBus;

public class Global : HttpApplication
{
    public static IBus Bus;

    protected void Application_Start(object sender, EventArgs e)
    {
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.AsyncPages.WebApplication");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();

        Bus = NServiceBus.Bus.Create(busConfiguration).Start();
    }

    protected void Application_End(object sender, EventArgs e)
    {
        Bus.Dispose();
    }

}