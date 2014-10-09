using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    using NServiceBus;
    using NServiceBus.Installation.Environments;

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IBus _bus;

        private IStartableBus _startableBus;

        public static IBus Bus
        {
            get { return _bus; }
        }

        protected void Application_Start()
        {
            Configure.ScaleOut(s => s.UseSingleBrokerQueue());

            _startableBus = Configure.With()
                .DefaultBuilder()
                .UseTransport<Msmq>()
                .UnicastBus()
                .RunGateway() //this line configures the gateway
                 .FileShareDataBus(".\\databus")
                .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

            _bus = _startableBus.Start();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            _startableBus.Dispose();
        }
    }
}