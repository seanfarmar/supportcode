namespace WebApi
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using NServiceBus;
    using NServiceBus.Installation.Environments;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            NServiceBusBootstrapper.Bootstrap();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public static class NServiceBusBootstrapper
    {
        private static IStartableBus _startableBus;

        private static IBus _bus;

        public static IBus Bus
        {
            get { return _bus; }
        }

        public static void Bootstrap()
        {
            _startableBus = Configure.With()
                 .DefineEndpointName("Plexis.Sample.WebService.Input")
                 .Log4Net()
                 .DefaultBuilder()
                 .XmlSerializer("http://plexissample.com")
                 .MsmqTransport()
                 .UnicastBus()
                 .CreateBus();
            Configure.Instance.ForInstallationOn<Windows>().Install();

            _bus = _startableBus.Start();
        }
    }
}