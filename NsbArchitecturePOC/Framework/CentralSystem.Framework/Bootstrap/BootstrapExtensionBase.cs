using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Bootstrap
{
    /// <summary>
    /// Base extension empty implementation
    /// </summary>
    public abstract class BootstrapExtensionBase : IBootstrapExtension 
    {
        /// <summary>
        /// Service container for initialization
        /// </summary>
        public IServiceContainer ServiceContainer { get; set; }

        /// <summary>
        /// Configure command
        /// </summary>
        public virtual void Configure() { }

        /// <summary>
        /// Start command
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Stop command
        /// </summary>
        public virtual void Stop() { }
    }
}
