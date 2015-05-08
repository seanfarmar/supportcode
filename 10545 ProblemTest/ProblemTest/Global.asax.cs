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
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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

        protected void Application_EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}
