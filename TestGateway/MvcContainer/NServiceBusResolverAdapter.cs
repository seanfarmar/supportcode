namespace MvcContainer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using NServiceBus;
    using NServiceBus.ObjectBuilder;

    public class NServiceBusDependencyResolverAdapter : IDependencyResolver
    {
        private readonly IBuilder _builder;

        public NServiceBusDependencyResolverAdapter(IBuilder builder)
        {
            _builder = builder;
        }

        public object GetService(Type serviceType)
        {
            if (Configure.Instance.Configurer.HasComponent(serviceType))
                return _builder.Build(serviceType);
            return null;
        }

        IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
        {
            return _builder.BuildAll(serviceType);
        }

        public IEnumerable GetServices(Type serviceType)
        {
            return _builder.BuildAll(serviceType);
        }
    }
}