namespace CentralSystem.DataObjects.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface provides option to override transaction mechanism to enlist
    /// commands from differenced frameworks:
    /// - ADO DOT.NET and NHibernet
    /// </summary>
    public interface ITransactionBridge
    {

        /// <summary>
        /// Database connection
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Enlist the System.Data.IDbCommand in the current Transaction.
        /// </summary>
        /// <param name="command">The System.Data.IDbCommand to enlist.</param>
        void Enlist(IDbCommand command);

        /// <summary>
        /// Force the underlying transaction to roll back.
        /// Implementation in runtime will not executed.
        /// Current function used only in unit test scenarios (not in NHibernate context).
        /// </summary>
        void Rollback();

    }
}
