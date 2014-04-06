
namespace IntegrationGateWay.DataBaseSite
{
    using NServiceBus;

    /*
        This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
        can be found here: http://particular.net/articles/the-nservicebus-host     
    */

    // Make sure you run this endpoint with the NServiceBus.Integration NServiceBus.MultiSite arguments

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
    }
}
