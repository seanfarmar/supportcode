namespace MessageConventions
{
    using NServiceBus;

    public class MyMessageConventions :IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.Conventions()
               .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Equals("MyCorp.NSB.Contracts.Commands"))
               .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Equals("MyCorp.NSB.Contracts.Events"));
        }
    }
}
