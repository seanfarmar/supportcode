namespace CentralSystem.Framework.NServiceBus.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::NServiceBus;

    /// <summary>
    /// Bus adapter initialization.
    /// Used for automatic bus initialization by NServiceHost windows service assembly
    /// </summary>
    public sealed class BusAdapterInitializer : BusAdapter, IWantToRunWhenBusStartsAndStops
    {

        #region Properties

        /// <summary>
        /// Created bus
        /// </summary>
        public IBus Bus { get; set; }

        #endregion

        #region IWantToRunWhenBusStartsAndStops Members

        /// <summary>
        /// Initialize bus adapter Bus property on start application
        /// </summary>
        public void Start()
        {
            SendOnlyBus = Bus;
        }

        /// <summary>
        /// Stop event
        /// </summary>
        public void Stop()
        {
        }

        #endregion
    }
}
