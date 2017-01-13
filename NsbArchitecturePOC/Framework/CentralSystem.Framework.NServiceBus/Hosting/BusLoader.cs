namespace CentralSystem.Framework.NServiceBus.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Consts;
    using global::NServiceBus;
    using global::NServiceBus.Logging;

    /// <summary>
    /// Bus loader
    /// </summary>
    public class BusLoader : BusAdapter
    {

        #region Members

        /// <summary>
        /// Endpoint config
        /// </summary>
        private readonly IConfigureThisEndpoint m_endpointConfig = null;

        /// <summary>
        /// Bus configuration
        /// </summary>
        private readonly BusConfiguration m_busConfiguration = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Bus loader initialization in mode of Send Only
        /// </summary>
        public BusLoader() : this(new BusConfiguration())
        {
        }

        /// <summary>
        /// Bus loader initialization in mode of Send Only
        /// </summary>
        /// <param name="busConfiguration">Bus configuration</param>
        public BusLoader(BusConfiguration busConfiguration)
        {
            m_busConfiguration = busConfiguration;
        }

        /// <summary>
        /// Bus loader
        /// </summary>
        /// <param name="endpointConfig">Endpoint config</param>
        public BusLoader(IConfigureThisEndpoint endpointConfig) : this()
        {
            m_endpointConfig = endpointConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start bus if not loaded.
        /// This function will be executed in the context of Application global.asax
        /// for initialization of bus if application was configured always running.
        /// </summary>
        public void StartSendOnlyBusIfNotStarted()
        {
            if (SendOnlyBus != null)
            {
                return;
            }
            StartSendOnlyBus();
        }

        /// <summary>
        /// Load and start bus
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void LoadAndStartBus()
        {
            //Endpoint configuration
            m_endpointConfig.Customize(m_busConfiguration);

            //Create and start bus
            SendOnlyBus = Bus.Create(m_busConfiguration).Start();
        }

        /// <summary>
        /// Start send only bus
        /// </summary>
        public void StartSendOnlyBus()
        {
            //Initialize endpoint configuration
            m_busConfiguration.SetSqlTransport(true);

            //Create and start bus
            SendOnlyBus = Bus.CreateSendOnly(m_busConfiguration);
        }

        #endregion

    }
}
