namespace PortalWebAccess
{
    using System;
    using System.Web;
    using NServiceBus;
    using NServiceBus.Features;
    using Security;

    public class Global : HttpApplication
    {
        public static IBus Bus { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            var configuration = new BusConfiguration();
            configuration.DisableFeature<CriticalTimeMonitoring>();
            configuration.DisableFeature<SLAMonitoring>();
            configuration.LoadMessageHandlers<First<UserAuthenticatedHandler>>();
            configuration.UsePersistence<InMemoryPersistence>();

            Bus = NServiceBus.Bus.Create(configuration).Start();
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