using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Bootstrap
{
    /// <summary>
    /// Bootstrapper 
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// Service container
        /// </summary>
        IServiceContainer ServiceContainer { get; } 

        /// <summary>
        /// Register extension
        /// </summary>
        /// <param name="extention">Extension</param>
        /// <returns>Bootstrapper</returns>
        IBootstrapper RegisterExtension(IBootstrapExtension extention);

        /// <summary>
        /// Run application
        /// </summary>
        void Run();

        /// <summary>
        /// Shutdown application
        /// </summary>
        void Shutdown();
    }
}
