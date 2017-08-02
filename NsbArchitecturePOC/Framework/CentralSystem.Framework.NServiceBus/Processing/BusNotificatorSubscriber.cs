namespace CentralSystem.Framework.NServiceBus.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Diagnostics;
    using CustomPerformanceCounters = CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters;
    using global::NServiceBus;
    using global::NServiceBus.Pipeline;

    /// <summary>
    /// Subscriber to bus notifications.
    /// Link: http://docs.particular.net/nservicebus/errors/subscribing-to-push-based-error-notifications.
    /// The class responsible for subscription of any differenced event handlers:
    /// - Performance counters integration.
    /// 
    /// Registration should be executed by BusConfigurationExtensions.
    /// </summary>
    internal sealed class BusNotificatorSubscriber : IWantToRunWhenBusStartsAndStops, IDisposable
    {

        #region Members

        /// <summary>
        /// Unsubscribed streams
        /// </summary>
        private readonly List<IDisposable> m_unsubscribeStreams = new List<IDisposable>();

        #endregion

        #region Constructors

        /// <summary>
        /// Main default constructor
        /// </summary>
        public BusNotificatorSubscriber()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Bus notifications
        /// </summary>
        public BusNotifications BusNotificator { get; set; }

        /// <summary>
        /// Bus receive started event subscriber to performance counters
        /// </summary>
        public CustomPerformanceCounters.BusReceiveStartedEventSubscriber BusReceiveStartedEventSubscriber { get; set; }

        #endregion

        #region IWantToRunWhenBusStartsAndStops Members

        /// <summary>
        /// Start subscription to bus pontificator
        /// </summary>
        public void Start()
        {
            //If registered then subscribe
            if (BusReceiveStartedEventSubscriber != null)
            {
                m_unsubscribeStreams.Add(
                    BusNotificator.Pipeline.ReceiveStarted.Subscribe(BusReceiveStartedEventSubscriber));
            }
        }

        /// <summary>
        /// Stop subscription
        /// </summary>
        public void Stop()
        {
            foreach (IDisposable unsubscribeStream in m_unsubscribeStreams)
            {
                unsubscribeStream.Dispose();
            }
            m_unsubscribeStreams.Clear();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        #endregion

    }
}
