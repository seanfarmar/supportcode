using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Raven.Client;
using StructureMap;

namespace ProblemTest
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
            //NSB
            _startableBus = Configure.With()
                .StructureMapBuilder()
                .RavenPersistence("RavenDB")
                .UseTransport<ActiveMQ>()
                .DefineEndpointName("IS.Argus.Core.Web")
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Command"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Event"))
                .DefiningMessagesAs(t => t.Namespace == "Messages")
                .UnicastBus()
                .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

            _bus = _startableBus.Start();

            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container)); // for MVC
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalHost.DependencyResolver = new StructureMapSignalRDependencyResolver(ObjectFactory.Container); // for signalR
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ObjectFactory.Container.Configure(x =>
            {
                x.For<IControllerActivator>().Use<StructureMapControllerActivator>();
                x.For<IFilterProvider>().Use<StructureMapFilterProvider>();                
                x.SetAllProperties(p => p.OfType<IAsyncDocumentSession>());
                x.Scan(scan =>
                {
                    scan.LookForRegistries();
                    scan.Assembly("ProblemTest");
                });
            });           
        }

        protected void Application_End()
        {
            _startableBus.Dispose();
        }

        protected void Application_EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}
