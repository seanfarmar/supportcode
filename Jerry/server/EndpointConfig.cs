
namespace server
{
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Saga;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
	    public void Init()
	    {
	        Configure.Features.Disable<TimeoutManager>();
	        Configure.Features.Disable<Sagas>();
	        Configure.Features.Disable<MessageDrivenSubscriptions>();

	    }
    }
}
