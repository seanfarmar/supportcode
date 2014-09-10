namespace MessageConventions
{
    using NServiceBus;

    class UnobtrusiveMessageConventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.Instance
                //.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusMessages") && t.Namespace.EndsWith("Commands"))
                //.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusMessages") && t.Namespace.EndsWith("Events"))
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusMessages") && t.Namespace.EndsWith("Messages"));
        }
    }
}
