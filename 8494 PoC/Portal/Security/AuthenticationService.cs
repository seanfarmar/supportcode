using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationGateway.Contracts;
using NServiceBus;

namespace Security
{
    public class AuthenticationService
    {
        private IBus _bus { get; set; }

        public AuthenticationService(IBus bus)
        {
            if (bus == null) throw new ArgumentNullException("bus");
            _bus = bus;
        }

        public void Authenticate(string username, string passsword)
        {
            _bus.Send(new NeedAuthentication() { Username = username, Password = passsword });
        }

    }
}
