namespace Conventions
{
    using NServiceBus;

    public class MessageConventions : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.Instance
                .DefiningMessagesAs(t => t.Namespace != null && (t.Namespace != null && t.Namespace.Contains("Server.Messages") || t.Namespace.Contains("Client.Messages")));
        } 
    }
}