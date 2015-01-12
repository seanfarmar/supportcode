using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationGateway.Contracts;
using NServiceBus;

namespace AuthenticationGateway.Handlers
{
    public class NeedAuthenticationHandler : IHandleMessages<NeedAuthentication>
    {
        public IBus Bus { get; set; }
        //public IDocumentSession session { get; set; }

        public void Handle(NeedAuthentication message)
        {
            Console.WriteLine("Username to be authenticated: " + message.Username);
            Bus.Publish(new UserAuthenticated() { Username = message.Username, Time = new DateTime() });
        }


    }
}
