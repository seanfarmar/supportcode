namespace PayrollGenerator.Saga
{
    using NServiceBus;

    internal class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }
}