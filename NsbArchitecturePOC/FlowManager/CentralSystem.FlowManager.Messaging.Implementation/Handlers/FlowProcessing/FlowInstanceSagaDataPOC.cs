namespace CentralSystem.FlowManager.Messaging.Implementation.Handlers.FlowProcessing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.FlowManager;
    using NServiceBus.Saga;
    using NServiceBus.SagaPersisters.NHibernate;

    /// <summary>
    /// Saga data of specific flow instance
    /// </summary>
    [LockMode(LockModes.Read)]
    //[TableName("FlowInstanceSagaDataPOC")]
    public class FlowInstanceSagaDataPOC : IContainSagaData
    {

        #region IContainSagaData Members

        /// <summary>
        /// Gets/sets the Id of the process. Do NOT generate this value in your code.
        /// The value of the Id will be generated automatically to provide the best
        /// performance for saving in a database.
        /// </summary>
        /// <remarks>
        /// The reason Guid is used for process Id is that messages containing this Id
        /// need to be sent by the process even before it is persisted.
        /// </remarks>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Contains the Id of the message which caused the saga to start.  This is needed
        /// so that when we reply to the Originator, any registered callbacks will be
        /// fired correctly.
        /// </summary>
        public virtual string OriginalMessageId { get; set; }
        
        /// <summary>
        /// Contains the return address of the endpoint that caused the process to run.
        /// </summary>
        public virtual string Originator { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Flow Instance ID
        /// </summary>
        [Unique]
        public virtual int FlowInstanceID { get; set; }

        /// <summary>
        /// Root object ID (draw ID, ...)
        /// </summary>
        public virtual int RootObjectID { get; set; }

        /// <summary>
        /// Flow type according to root object type
        /// </summary>
        public virtual string RootObjectType { get; set; }

        /// <summary>
        /// Row Version - NSB saga integration with optimistic lock
        /// </summary>
        [RowVersion]
        public virtual int Version { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public virtual short BrandID { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Use for log data
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("FlowInstanceID={0}, RootObjectType={1}, RootObjectID={2}",
                FlowInstanceID, RootObjectType, RootObjectID);
        }

        #endregion

    }
}
