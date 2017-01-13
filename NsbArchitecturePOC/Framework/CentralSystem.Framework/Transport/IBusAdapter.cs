namespace CentralSystem.Framework.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// API definition for Bus integration to send messages
    /// </summary>
    public interface IBusAdapter
    {

        /// <summary>
        /// Gets the list of key/value pairs that will be in the header of messages being
        /// sent by the same thread.  This value will be cleared when a thread receives
        /// a message.
        /// </summary>
        IDictionary<string, string> OutgoingHeaders { get; }
        
        /// <summary>
        /// Send message to bus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        void Send<TMessage>(TMessage message);

        /// <summary>
        /// Send message to bus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="address">Address</param>
        /// <param name="message">Message</param>
        void Send<TMessage>(string address, TMessage message);

        /// <summary>
        /// Send message to bus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        void SendLocal<TMessage>(TMessage message);

        /// <summary>
        /// Integration with NSB ISendOnlyBus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        /// <param name="callback">The callback to invoke</param>
        /// <param name="state">State that will be passed to the callback method</param>
        /// <returns>An IAsyncResult useful for integration with asynchronous execution</returns>
        IAsyncResult SendAndRegisterCallback<TMessage>(TMessage message, Action<int> callback, object state);

        /// <summary>
        /// Integration with NSB ISendOnlyBus
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">Message</param>
        /// <param name="callback">The callback to invoke</param>
        /// <param name="state">State that will be passed to the callback method</param>
        /// <returns>An IAsyncResult useful for integration with asynchronous execution</returns>
        IAsyncResult SendLocalAndRegisterCallback<TMessage>(TMessage message, Action<int> callback, object state);

    }
}
