namespace Security
{
    using System.Diagnostics;
    using AuthenticationGateway.Contracts;
    using NServiceBus;

    public class UserAuthenticatedHandler : IHandleMessages<UserAuthenticated>
    {
        public void Handle(UserAuthenticated message)
        {
            Debug.WriteLine("User authenticated: " + message.Username);
        }
    }
}