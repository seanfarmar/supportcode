namespace CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Consts;
    using global::NServiceBus;
    using global::NServiceBus.Pipeline;

    /// <summary>
    /// Event subscriber to message receive started event to activate relevant performance counter
    /// </summary>
    internal sealed class BusReceiveStartedEventSubscriber : IObserver<IObservable<StepStarted>>
    {

        #region Members

        /// <summary>
        /// Logger
        /// </summary>
        private readonly static global::NServiceBus.Logging.ILog s_logger = global::NServiceBus.Logging.LogManager.GetLogger(typeof(BusReceiveStartedEventSubscriber));

        #endregion

        #region IObserver<IObservable<StepStarted>> Members

        /// <summary>
        /// Complete event
        /// </summary>
        public void OnCompleted()
        {
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="error"></param>
        public void OnError(Exception error)
        {
        }

        /// <summary>
        /// Next event
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(IObservable<StepStarted> value)
        {
            try
            {
                if (BusCustomPerformanceCounters.EndpointIsActiveCreator != null) BusCustomPerformanceCounters.EndpointIsActiveCreator().RawValue = System.Int32.MaxValue;
            }
            catch (Exception ex)
            {
                s_logger.Error("Error on attempt to activate performance counters", ex);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set as inactive endpoint
        /// </summary>
        internal static void MarkEndPointAsInactive()
        {
            try
            {
                if (BusCustomPerformanceCounters.EndpointIsActiveCreator != null) BusCustomPerformanceCounters.EndpointIsActiveCreator().RawValue = 0;
            }
            catch (Exception ex)
            {
                s_logger.Error("Error on attempt to activate performance counters", ex);
            }
        }

        #endregion
    
    }
}
