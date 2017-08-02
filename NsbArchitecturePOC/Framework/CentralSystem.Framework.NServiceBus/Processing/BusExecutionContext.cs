namespace CentralSystem.Framework.NServiceBus.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.Exceptions;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Diagnostics;
    using CentralSystem.Framework.NServiceBus.Exceptions;

    /// <summary>
    /// Bus execution context API internal implementation.
    /// IMPORTANT:
    ///     Registration should be executed by BusConfigurationExtensions.
    ///     Registration of class in the container is DependencyLifecycle.InstancePerUnitOfWork.
    ///     The instance will be singleton for the duration of the unit of work. In practice
    ///     this means the processing of a single transport message.
    /// </summary>
    internal sealed class BusExecutionContext : IBusExecutionContext
    {

        #region Members

        /// <summary>
        /// Logger
        /// </summary>
        private readonly static global::NServiceBus.Logging.ILog s_logger = global::NServiceBus.Logging.LogManager.GetLogger(typeof(BusExecutionContext));

        /// <summary>
        /// List of context items dictionary
        /// </summary>
        private readonly Dictionary<System.Type, IDisposable> m_contextItemsDictionary = new Dictionary<Type, IDisposable>();

        /// <summary>
        /// Create date time
        /// </summary>
        public DateTime m_createdDateTime = DateTime.Now;

        /// <summary>
        /// True - current execution context is in handlers mode (processing of handler).
        /// False - context created in not handlers execution (send message not from handler, in separated thread).
        /// </summary>
        private bool m_isInHandleMessageContext = false;

        #endregion

        #region IBusExecutionContext Members

        /// <summary>
        /// Create date time
        /// </summary>
        public DateTime CreatedDateTime 
        { 
            get
            {
                return m_createdDateTime;
            }
        }

        /// <summary>
        /// True - current execution context is in handlers mode (processing of handler).
        /// False - context created in not handlers execution (send message not from handler, in separated thread).
        /// </summary>
        public bool IsInHandleMessageContext
        {
            get
            {
                return m_isInHandleMessageContext;
            }
        }

        /// <summary>
        /// Register context item
        /// </summary>
        /// <typeparam name="TContextItem">Context item type. Requirement - item should be disposable</typeparam>
        /// <param name="contextItem">Context item</param>
        public void AddItem<TContextItem>(TContextItem contextItem) where TContextItem : IDisposable
        {
            if (contextItem == null)
            {
                throw new ArgumentNullException("contextItem", "Unsupported NULL context item");
            }
            m_contextItemsDictionary.Add(typeof(TContextItem), contextItem);
        }

        /// <summary>
        /// Find context item by type
        /// </summary>
        /// <typeparam name="TContextItem">Context item type. Requirement - item should be disposable</typeparam>
        /// <returns>Found item or NULL</returns>
        public TContextItem GetItem<TContextItem>() where TContextItem : IDisposable
        {
            IDisposable resultContextItem;
            m_contextItemsDictionary.TryGetValue(typeof(TContextItem), out resultContextItem);
            return (TContextItem)resultContextItem;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start execution context
        /// </summary>
        internal void BeginUnitOfWorks()
        {
            if (m_contextItemsDictionary.Count != 0)
            {
                EndUnitOfWorks();
                throw new CentralSystemException(
                    NSBCommonSettings.NSERVICE_BUS_LOGGER_DEFAULT_CODE,
                    "Begin event error: Bus execution context already contains not disposed objects. Before start processing will be initiated Dispose() for old context items.");
            }
            m_isInHandleMessageContext = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// Analogy of Dispose function.
        /// The reason don't reuse IDisposable interface:
        /// - Managing should be executed only from BusTransportMessageManageUnitsOfWork
        /// - Release should be executed in the context of logger context before cleaning
        /// - The function can't be visible by consumer applications
        /// </summary>
        internal void EndUnitOfWorks()
        {
            foreach (KeyValuePair<System.Type, IDisposable> contextItemKeyValue in m_contextItemsDictionary)
            {
                try
                {
                    contextItemKeyValue.Value.Dispose();
                }
                catch (Exception ex)
                {
                    s_logger.Error(
                        string.Format("Disposed with error bus execution context item type named '{0}'", contextItemKeyValue.Key.Name),
                        ex);
                }
            }
            m_contextItemsDictionary.Clear();
            m_isInHandleMessageContext = false;
        }

        #endregion

    }
}
