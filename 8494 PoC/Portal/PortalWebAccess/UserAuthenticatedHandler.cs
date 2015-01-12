using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationGateway.Contracts;
using NServiceBus;

namespace Security
{
    public class UserAuthenticatedHandler : IHandleMessages<UserAuthenticated>
    {
        public void Handle(UserAuthenticated message)
        {
            System.Diagnostics.Debug.WriteLine("User authenticated: " + message.Username);
        }
    }
}
