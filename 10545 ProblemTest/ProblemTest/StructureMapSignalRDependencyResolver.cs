using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using StructureMap;

namespace ProblemTest
{
    public class StructureMapSignalRDependencyResolver : DefaultDependencyResolver
    {
        public StructureMapSignalRDependencyResolver(IContainer container)
        {
            _container = container;
        }

        private readonly IContainer _container;      

        public override object GetService(Type serviceType)
        {

            if (serviceType == null)
                return null;

            return ((serviceType.IsAbstract || serviceType.IsInterface) && !serviceType.IsClass) 
                     ? _container.TryGetInstance(serviceType) ?? base.GetService(serviceType)
                     : _container.GetInstance(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var objects = _container.GetAllInstances(serviceType).Cast<object>();
            objects = objects.Concat(base.GetServices(serviceType));
            return objects;
        }

    }
}