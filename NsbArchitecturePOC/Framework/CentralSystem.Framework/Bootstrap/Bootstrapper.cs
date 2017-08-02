using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Bootstrap
{
    public class Bootstrapper : IBootstrapper 
    {

        public Bootstrapper()
        {
            this.ServiceContainer = CentralSystem.Framework.ServiceContainer.Current;
        }

        public Bootstrapper(IServiceContainer serviceContainer)
        {
            this.ServiceContainer = serviceContainer;
        }

        public IServiceContainer ServiceContainer { get; private set; }
        
        public IBootstrapper RegisterExtension(IBootstrapExtension extention)
        {
            extention.ServiceContainer = this.ServiceContainer;
            return this;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

    }
}
