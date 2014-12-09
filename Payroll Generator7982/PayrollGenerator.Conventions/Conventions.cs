namespace PayrollGenerator.Conventions
{
    using NServiceBus;

    public class Conventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.With()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Messages.Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Messages.Events"))
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith("Messages.Response"));
        }
    }
}