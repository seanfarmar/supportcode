namespace AuthenticationGateway.Contracts
{
    using NServiceBus;

    public class NeedAuthentication : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}