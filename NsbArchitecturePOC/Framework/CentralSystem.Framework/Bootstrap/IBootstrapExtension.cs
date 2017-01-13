using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Bootstrap
{
    /// <summary>
    /// IBootstrap extension definition
    /// </summary>
    public interface IBootstrapExtension
    {
        /// <summary>
        /// Service container
        /// </summary>
        IServiceContainer ServiceContainer { get; set; }

        /// <summary>
        /// Configure command
        /// </summary>
        void Configure();

        /// <summary>
        /// Start command
        /// </summary>
        void Start();

        /// <summary>
        /// Stop command
        /// </summary>
        void Stop();
    }
}
