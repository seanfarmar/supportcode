namespace WebApplication_mvc5
{
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Installation.Environments;

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IBus bus;

        private IStartableBus startableBus;

        public static IBus Bus
        {
            get { return bus; }
        }

        protected void Application_Start()
        {
            //ConfigureStartableBus();
            ConfigureStartableBusXP();

            bus = startableBus.Start();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        private void ConfigureStartableBusXP()
        {
            Configure.Transactions.Enable();
            Configure.Serialization.Json();
            Configure.Features.Disable<Sagas>();
            Feature.Disable<XmlSerialization>();

            startableBus = Configure.With(
               AllAssemblies.Matching("Messages.dll").And("Handlers.dll"))
               // .DefineEndpointName(ConfigurationManager.AppSettings["ServiceBusEndpointName"])
               .DefaultBuilder()
               .DefiningMessagesAs(t => t.Namespace == "Messages")
               .RavenSubscriptionStorage()
               .UseTransport<Msmq>()
               .PurgeOnStartup(false)
               .UnicastBus()
               .RunHandlersUnderIncomingPrincipal(false)
               .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();
        }

        private void ConfigureStartableBus()
        {
            Configure.ScaleOut(s => s.UseSingleBrokerQueue());

            startableBus = Configure.With()
                .DefaultBuilder()
                .DefiningMessagesAs(t => t.Namespace == "Messages")
                .UseTransport<Msmq>()
                .PurgeOnStartup(true)
                .UnicastBus()
                .RunHandlersUnderIncomingPrincipal(false)
                .RijndaelEncryptionService()
                .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();
        }
    }

    //public class Conventions : IWantToRunBeforeConfiguration
    //{
    //    public void Init()
    //    {
    //        Configure.Instance.DefiningMessagesAs(t => t.Namespace == "Messages");
    //    }
    //}
}
