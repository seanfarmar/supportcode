using System;
using NServiceBus;

namespace AuthenticationGateway.Contracts
{
    public class NeedAuthentication : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
