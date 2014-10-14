using NServiceBus;
using NServiceBus.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BmcSiteA
{
    public static class ConfigureMvc3
    {
        public static NServiceBus.Configure ForMVC(this NServiceBus.Configure configure)
        {
            // Register our controller activator with NSB
            configure.Configurer.RegisterSingleton(typeof(IControllerActivator),
                new NServiceBusControllerActivator());

            var controllers = NServiceBus.Configure.TypesToScan.Where(t => typeof(IController).IsAssignableFrom(t));

            // Register each controller class with the NServiceBus container
            foreach (Type type in controllers)
                configure.Configurer.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);

            // Set the MVC dependency resolver to use our resolver
            DependencyResolver.SetResolver(new NServiceBusDependencyResolverAdapter(configure.Builder));

            // Required by the fluent configuration semantics
            return configure;
        }
    }
}