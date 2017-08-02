namespace CentralSystem.Framework.NServiceBus.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.Transport;
    using global::NServiceBus;

    /// <summary>
    /// Bus adapter
    /// </summary>
    public class BusAdapter : IBusAdapter
    {

        #region Members

        /// <summary>
        /// Bus instance
        /// </summary>
        private static ISendOnlyBus s_sendOnlyBus = null;

        #endregion

        #region Properties

        /// <summary>
        /// Send only bus
        /// </summary>
        internal ISendOnlyBus SendOnlyBus
        {
            get
            {
                return s_sendOnlyBus;
            }
            set
            {
                s_sendOnlyBus = value;
            }
        }

        #endregion

        #region IBusAdapter Members

        /// <summary>
        /// Gets the list of key/value pairs that will be in the header of messages being
        /// sent by the same thread.  This value will be cleared when a thread receives
        /// a message.
        /// </summary>
        public IDictionary<string, string> OutgoingHeaders 
        { 
            get
            {
                return ResolveSendOnlyBus().OutgoingHeaders;
            }
        }

        /// <summary>
        /// Integration with NSB ISendOnlyBus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        public void Send<TMessage>(TMessage message)
        {
            ResolveSendOnlyBus().Send(message);
        }

        /// <summary>
        /// Send message to bus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="address">Address</param>
        /// <param name="message">Message</param>
        public void Send<TMessage>(string address, TMessage message)
        {
            ResolveSendOnlyBus().Send(address, message);
        }

        /// <summary>
        /// Send message to bus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        public void SendLocal<TMessage>(TMessage message)
        {
            ResolveFullBus().SendLocal(message);
        }

        /// <summary>
        /// Integration with NSB ISendOnlyBus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        /// <param name="callback">The callback to invoke</param>
        /// <param name="state">State that will be passed to the callback method</param>
        /// <returns>An IAsyncResult useful for integration with asynchronous execution</returns>
        public IAsyncResult SendAndRegisterCallback<TMessage>(TMessage message, Action<int> callback, object state)
        {
            return ResolveSendOnlyBus().Send(message)
                .Register(ar =>
                {
                    CompletionResult localResult = (CompletionResult)ar.AsyncState;
                    callback(localResult.ErrorCode);
                }, state);
        }

        /// <summary>
        /// Integration with NSB ISendOnlyBus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        /// <param name="callback">The callback to invoke</param>
        /// <param name="state">State that will be passed to the callback method</param>
        /// <returns>An IAsyncResult useful for integration with asynchronous execution</returns>
        public IAsyncResult SendLocalAndRegisterCallback<TMessage>(TMessage message, Action<int> callback, object state)
        {
            return ResolveFullBus().SendLocal(message)
                .Register(ar => 
                    {
                        CompletionResult localResult = (CompletionResult)ar.AsyncState;
                        callback(localResult.ErrorCode);
                    }, state);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Full bus reference
        /// </summary>
        private IBus ResolveFullBus()
        {
            IBus fullBus = SendOnlyBus as IBus;
            if (fullBus == null)
            {
                throw new InvalidOperationException("Invalid operation for current bus");
            }
            return fullBus;
        }

        /// <summary>
        /// Send only bus reference
        /// </summary>
        private ISendOnlyBus ResolveSendOnlyBus()
        {
            if (SendOnlyBus == null)
            {
                throw new InvalidOperationException("Invalid operation for current bus");
            }
            return SendOnlyBus;
        }

        #endregion

    }
}
