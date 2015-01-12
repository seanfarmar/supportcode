namespace Security
{
    using System;
    using AuthenticationGateway.Contracts;
    using NServiceBus;

    public class AuthenticationService
    {
        public IBus _bus { get; set; }

        public AuthenticationService(IBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");
            _bus = bus;
        }

        public void Authenticate(string username, string passsword)
        {
            _bus.Send(new NeedAuthentication {Username = username, Password = passsword});
        }
    }
}