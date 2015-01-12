namespace AuthenticationGateway.Contracts
{
    using System;
    using NServiceBus;

    public class UserAuthenticated : IEvent
    {
        public string Username { get; set; }
        public DateTime Time { get; set; }
    }
}