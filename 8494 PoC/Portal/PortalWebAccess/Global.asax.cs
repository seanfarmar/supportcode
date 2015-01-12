using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using NServiceBus;

namespace PortalWebAccess
{
    public class Global : System.Web.HttpApplication
    {
        public static IBus Bus { get; private set; }
        protected void Application_Start(object sender, EventArgs e)
        {
            var configuration = new BusConfiguration();
            configuration.DisableFeature<NServiceBus.Features.CriticalTimeMonitoring>();
            configuration.DisableFeature<NServiceBus.Features.SLAMonitoring>();
            configuration.LoadMessageHandlers<First<Security.UserAuthenticatedHandler>>();
            configuration.UsePersistence<InMemoryPersistence>();
            Bus = NServiceBus.Bus.Create(configuration);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}