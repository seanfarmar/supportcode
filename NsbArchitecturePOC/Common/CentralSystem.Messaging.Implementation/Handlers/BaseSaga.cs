namespace CentralSystem.Messaging.Implementation.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.DataObjects.ValueObjects;
    using CentralSystem.Framework.NServiceBus;
    using CentralSystem.Messaging.Implementation.NHibernate;
    using NServiceBus.Persistence.NHibernate;
    using NServiceBus.Saga;

    /// <summary>
    /// Base saga handlers
    /// </summary>
    /// <typeparam name="TSagaData">Saga data</typeparam>
    public abstract class BaseSaga<TSagaData> : Saga<TSagaData>
        where TSagaData : NServiceBus.Saga.IContainSagaData, new()
    {

        #region Properties

        /// <summary>
        /// Bus execution context
        /// </summary>
        public IBusExecutionContext BusExecutionContext { get; set; }

        /// <summary>
        /// NHybernet storage context
        /// </summary>
        public NHibernateStorageContext NHibernateStorageContext { get; set; }

        /// <summary>
        /// Bridge to specific implementation of transaction from NHibernate context
        /// </summary>
        protected ITransactionBridge TransactionBridge
        {
            get
            {
                if (NHibernateStorageContext != null && NHibernateStorageContext.Session != null)
                {
                    return new NHibernateJointTransactionContext(NHibernateStorageContext);
                }
                return null;
            }

        }

        #endregion

    }
}
