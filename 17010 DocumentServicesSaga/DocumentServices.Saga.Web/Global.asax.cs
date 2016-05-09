using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace DocumentServices.Saga.Web
{
    public class MvcApplication : HttpApplication
    {
        public static IBus Bus;

        public override void Dispose()
        {
            if (Bus != null)
            {
                Bus.Dispose();
            }
            base.Dispose();
        }

        protected void Application_Start()
        {
            var configuration = new BusConfiguration();
            configuration.EndpointName("DocumentServices.Saga.Web");
            configuration.ApplyCommonConfiguration();

            Bus = NServiceBus.Bus.Create(configuration).Start();


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    class ConfigErrorQueue : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}
