using NServiceBus.ObjectBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BmcSiteA
{
    public class NServiceBusDependencyResolverAdapter : IDependencyResolver
    {
        private IBuilder builder;

        public NServiceBusDependencyResolverAdapter(IBuilder builder)
        {
            this.builder = builder;
        }
        public object GetService(Type serviceType)
        {
            if (NServiceBus.Configure.Instance.Configurer.HasComponent(serviceType))
                return builder.Build(serviceType);
            else
                return null;
        }

        public IEnumerable GetServices(Type serviceType)
        {
            return builder.BuildAll(serviceType);
        }

        IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
        {
            return builder.BuildAll(serviceType);
        }
    }
}