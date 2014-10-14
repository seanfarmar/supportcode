﻿using NServiceBus;
using NServiceBus.Installation.Environments;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BmcHeadquarter
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
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
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Configure.ScaleOut(s => s.UseSingleBrokerQueue());

            _startableBus = Configure.WithWeb()
                .DefaultBuilder()
                .ForMVC()
                .MsmqTransport()
                .UnicastBus()
                .RunGateway() //this line configures the gateway
                 .FileShareDataBus(".\\databus")
                .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

            _bus = _startableBus.Start();

            //Configure.WithWeb()
            //    .DefaultBuilder()
            //    .ForMVC()   // <------ here is the line that registers everything
            //    .Log4Net()
            //    .XmlSerializer()
            //    .MsmqTransport()
            //        .IsTransactional(false)
            //        .PurgeOnStartup(false)
            //    .UnicastBus()
            //        .ImpersonateSender(false)
            //    .RunGateway()
            //    .CreateBus()
            //    .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}