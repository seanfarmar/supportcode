namespace CentralSystem.DataObjects.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Request context object - container for objects cross layer.
    /// First candidate is NServiceBus SQL connection
    /// </summary>
    public sealed class RequestContext
    {

        #region Members

        /// <summary>
        /// Objects container
        /// </summary>
        private readonly Dictionary<string, object> m_objects;

        /// <summary>
        /// Standard key
        /// </summary>
        public const string KEY_TRANSACTION_BRIDGE = "TransactionBridgeCtx";

        /// <summary>
        /// Standard key
        /// </summary>
        private const string KEY_WAS_IDEMPOTENT_REQUEST_DETECTION = "WasIdempotentRequestDetectionCtx";

        /// <summary>
        /// Standard key
        /// </summary>
        private const string KEY_COMMITTED_TRANSACTION_COUNTER = "CommitedTransactionCounterCtx";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestContext()
        {
            m_objects = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Bridge to specific implementation of transaction (NServiceBus nHibernet)
        /// </summary>
        public ITransactionBridge TransactionBridge
        {
            get
            {
                return GetObject<ITransactionBridge>(KEY_TRANSACTION_BRIDGE);                
            }
            set
            {
                AddObject(KEY_TRANSACTION_BRIDGE, value);
            }
        }

        /// <summary>
        /// True - in the business request was idempotent request detection
        /// </summary>
        public bool DetectedIdempotentRequestWasCommited
        {
            get
            {
                return GetObject<bool>(KEY_WAS_IDEMPOTENT_REQUEST_DETECTION);
            }
            set
            {
                AddObject(KEY_WAS_IDEMPOTENT_REQUEST_DETECTION, value);
            }
        }

        /// <summary>
        /// Committed transaction counter
        /// </summary>
        public int CommittedTransactionCounter
        {
            get
            {
                return GetObject<int>(KEY_COMMITTED_TRANSACTION_COUNTER);
            }
            set
            {
                AddObject(KEY_COMMITTED_TRANSACTION_COUNTER, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save object in context
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="obj">Object</param>
        public void AddObject(string key, object obj)
        {
            m_objects[key] = obj;
        }

        /// <summary>
        /// Read object from context
        /// </summary>
        /// <param name="key">Key</param>
        public TObject GetObject<TObject>(string key)
        {
            if (!m_objects.ContainsKey(key))
            {
                return default(TObject);
            }
            return (TObject) m_objects[key];
        }

        #endregion

    }
}
