namespace MessageConventions
{
    using NServiceBus;

    public static class MyMessageConventionsExtentions
    {
        public static void ApplyMessageConventions(this BusConfiguration configuration)
        {
            configuration.Conventions()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Equals("MyCorp.NSB.Contracts.Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Equals("MyCorp.NSB.Contracts.Events"));
        }
    }
}