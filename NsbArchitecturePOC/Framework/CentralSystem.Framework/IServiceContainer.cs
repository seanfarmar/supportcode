using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework
{
    /// <summary>
    /// Service container definition
    /// </summary>
    public interface IServiceContainer
    {

        /// <summary>
        /// Register singleton service instance
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <param name="service">Instance service</param>
        /// <returns>Service container</returns>
        IServiceContainer Register<TService>(TService service);

        /// <summary>
        /// Register service factory function
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <param name="service">Instance service</param>
        /// <returns>Service container</returns>
        IServiceContainer Register<TService>(Func<TService> serviceFactory);

        /// <summary>
        /// Activate instance of service
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <returns>Service container</returns>
        TService GetInstance<TService>();

    }
}
