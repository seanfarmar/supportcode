using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework
{
    public class ServiceContainer : IServiceContainer
    {
        
        public static void SetServiceContainer(IServiceContainer serviceContainer)
        {
            throw new NotImplementedException();
        }

        public static IServiceContainer Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IServiceContainer Register<TService>(Func<TService> serviceFactory)
        {
            throw new NotImplementedException();
        }

        public IServiceContainer Register<TService>(TService service)
        {
            throw new NotImplementedException();
        }

        public TService GetInstance<TService>()
        {
            throw new NotImplementedException();
        }

    }
}
