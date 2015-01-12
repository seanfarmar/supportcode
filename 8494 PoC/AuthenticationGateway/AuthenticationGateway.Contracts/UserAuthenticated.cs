using System;
using NServiceBus;

namespace AuthenticationGateway.Contracts
{
    public class UserAuthenticated : IEvent
    {
        public string Username { get; set; }
        public DateTime Time { get; set; }
    }
}
