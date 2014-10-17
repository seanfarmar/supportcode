namespace BmcHeadquarter
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcContainer;
    using NServiceBus;
    using NServiceBus.Installation.Environments;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private static IBus _bus;

        private IStartableBus _startableBus;

        public static IBus Bus
        {
            get { return _bus; }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Configure.ScaleOut(s => s.UseSingleBrokerQueue());

            _startableBus = Configure.With()
                .DefaultBuilder()
                .ForMvc() // no need for this...
                .MsmqTransport()
                .UnicastBus()
                .RunGateway() //this line configures the gateway
                .FileShareDataBus(".\\databus")
                .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

            _bus = _startableBus.Start();
        }
    }
}