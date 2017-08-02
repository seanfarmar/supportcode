namespace CentralSystem.Framework.NServiceBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Bus execution context API interface
    /// </summary>
    public interface IBusExecutionContext
    {

        /// <summary>
        /// Create date time
        /// </summary>
        DateTime CreatedDateTime { get; }

        /// <summary>
        /// True - current execution context is in handlers mode (processing of handler).
        /// False - context created in not handlers execution (send message not from handler, in separated thread).
        /// </summary>
        bool IsInHandleMessageContext { get; }

        /// <summary>
        /// Register context item
        /// </summary>
        /// <typeparam name="TContextItem">Context item type. Requirement - item should be disposable</typeparam>
        /// <param name="contextItem">Context item</param>
        void AddItem<TContextItem>(TContextItem contextItem) where TContextItem : IDisposable;

        /// <summary>
        /// Find context item by type
        /// </summary>
        /// <typeparam name="TContextItem">Context item type. Requirement - item should be disposable</typeparam>
        /// <returns>Found item or NULL</returns>
        TContextItem GetItem<TContextItem>() where TContextItem : IDisposable;

    }
}
