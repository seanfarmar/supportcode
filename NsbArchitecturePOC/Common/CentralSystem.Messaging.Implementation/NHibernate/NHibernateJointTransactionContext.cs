namespace CentralSystem.Messaging.Implementation.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.DataObjects.ValueObjects;
    using global::NHibernate;
    using NServiceBus.Persistence.NHibernate;

    /// <summary>
    /// Integration with NHibernate transaction / connection mechanism.
    /// Current integration with NHibernate transaction should be delegated to business layer by messaging layer
    /// to allow to execute any business changes in the WorkFlows database in the context of NHibernate transaction.
    /// Rollback scenario does not supported.
    /// </summary>
    internal sealed class NHibernateJointTransactionContext : ITransactionBridge
    {

        #region Members

        /// <summary>
        /// NHibernate context
        /// </summary>
        private readonly ISession m_nHibernateSession;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="nHibernateStorageContext">NHibernet context</param>
        public NHibernateJointTransactionContext(NHibernateStorageContext nHibernateStorageContext)
        {
            if (nHibernateStorageContext == null || nHibernateStorageContext.Session == null)
            {
                throw new NotSupportedException("Required NHibernate session ");
            }
            m_nHibernateSession = nHibernateStorageContext.Session;
        }

        #endregion

        #region IJointTransaction Members

        /// <summary>
        /// Connection object
        /// </summary>
        public IDbConnection Connection
        {
            get 
            {
                return m_nHibernateSession.Connection as DbConnection;
            }
        }

        /// <summary>
        /// Enlist database command to transaction
        /// </summary>
        /// <param name="command">Command</param>
        public void Enlist(IDbCommand command)
        {
            m_nHibernateSession.Transaction.Enlist(command);
        }

        /// <summary>
        /// Only NServiceBus is responsible for decision to rollback transaction.
        /// Current method defined on interface level for unit testing.
        /// </summary>
        public void Rollback()
        {
            throw new NotSupportedException("In this NHibernate integration Central system can't get decision to rollback transaction!");
        }

        #endregion

    }
}
