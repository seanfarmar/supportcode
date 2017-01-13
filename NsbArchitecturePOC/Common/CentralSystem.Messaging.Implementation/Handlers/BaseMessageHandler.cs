namespace CentralSystem.Messaging.Implementation.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus;
    using NServiceBus;
    using NServiceBus.Persistence.NHibernate;

    /// <summary>
    /// Base class for message handlers
    /// </summary>
    public abstract class BaseMessageHandler
    {

        #region Properties

        /// <summary>
        /// Bus instance
        /// </summary>
        public IBus Bus { get; set; }

        /// <summary>
        /// Bus execution context
        /// </summary>
        public IBusExecutionContext BusExecutionContext { get; set; }

        #endregion

    }
}
