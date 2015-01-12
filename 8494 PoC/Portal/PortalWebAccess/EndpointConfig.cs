using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;

namespace PortalWebAccess
{
    public class EndpointConfig: IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
        }
    }
}