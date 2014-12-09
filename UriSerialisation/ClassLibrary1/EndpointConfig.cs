
namespace ClassLibrary1
{
    using System;
    using System.IO;
    using ClassLibrary2;
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
	    public void Customize(BusConfiguration configuration)
	    {
	        configuration.UsePersistence<InMemoryPersistence>();
	    }
    }

    class Starter : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            var uri = new Uri(@"http://docs.google.com/uc?authuser=1&id=0BzGD5JpB16DVTWNoemYyNkY3ZEk&ex");

            var myCommand = new MyCommand(10, uri);

            Bus.SendLocal(myCommand);
        }

        public void Stop()
        {
        }
    }
}
