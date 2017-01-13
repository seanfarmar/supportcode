namespace CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Exceptions;
    using global::NServiceBus.UnitOfWork;
    using NHibernate;

    /// <summary>
    /// Event subscriber to message processing events to activate relevant performance counters:
    /// - Concurrent
    /// - Total errors
    /// - Total success processed
    /// </summary>
    internal sealed class BusTransportMessageProcessingSubscriber
    {

        #region Members

        /// <summary>
        /// Logger
        /// </summary>
        private readonly static global::NServiceBus.Logging.ILog s_logger = global::NServiceBus.Logging.LogManager.GetLogger(typeof(BusTransportMessageProcessingSubscriber));
        
        /// <summary>
        /// For statistic to manage concurrency performance counter
        /// </summary>
        private int m_messagesInProgressPerformanceCounterCallCount = 0;

        #endregion

        #region Public Methods

        /// <summary>
        /// Begin event
        /// </summary>
        public void NotifyBeginProcess()
        {
            try
            {
                //To prevent for scenario if NServiceBus calls some times Begin event unit of works
                if (m_messagesInProgressPerformanceCounterCallCount == 0)
                {
                    if (BusCustomPerformanceCounters.TotalMessagesStartedProcessingCreator != null) BusCustomPerformanceCounters.TotalMessagesStartedProcessingCreator().Increment();

                    if (BusCustomPerformanceCounters.MessagesInProgressCreator != null)
                    {
                        BusCustomPerformanceCounters.MessagesInProgressCreator().Increment();
                    }

                    m_messagesInProgressPerformanceCounterCallCount++;

                }

            }
            catch (Exception ex)
            {
                s_logger.Error("Error on attempt to activate performance counters", ex);
            }
        }

        /// <summary>
        /// End event
        /// </summary>
        /// <param name="ex">Exception</param>
        public void NotifyEndProcess(Exception ex)
        {
            try
            {
                //Concurrency
                if (BusCustomPerformanceCounters.MessagesInProgressCreator != null)
                {
                    //To prevent for scenario if Begin event failed and not incremented performance counter
                    while (m_messagesInProgressPerformanceCounterCallCount > 0) 
                    {
                        BusCustomPerformanceCounters.MessagesInProgressCreator().Decrement();
                        m_messagesInProgressPerformanceCounterCallCount--;
                    }
                }

                if (ex == null)
                {
                    //For success scenario
                    if (BusCustomPerformanceCounters.TotalMessagesCompletedSuccessfullyCreator != null) BusCustomPerformanceCounters.TotalMessagesCompletedSuccessfullyCreator().Increment();

                }
                else if (IsUnhandledException(ex) == true) 
                {
                    //For error scenario (excluded optimistic lock exception)
                    if (BusCustomPerformanceCounters.TotalRetryErrorsCreator != null) BusCustomPerformanceCounters.TotalRetryErrorsCreator().Increment();

                }

            }
            catch (Exception exception)
            {
                s_logger.Error("Error on attempt to activate performance counters", exception);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Detect if exception is unhandled (retry able)
        /// </summary>
        /// <param name="ex">Checked exception</param>
        /// <returns>True - current exception is retry able</returns>
        private bool IsUnhandledException(Exception ex)
        {
            //Cancel retry mechanism exception scenario
            if (ex is CancelBusSecondLevelRetryException)
            {
                return false;
            }

            //Optimistic lock exception scenario
            if (ex is StaleObjectStateException)
            {
                return false;
            }

            NHibernate.Exceptions.GenericADOException nhibernetGenericADOException = ex as NHibernate.Exceptions.GenericADOException;
            if (nhibernetGenericADOException != null)
            {
                if (!string.IsNullOrEmpty(nhibernetGenericADOException.Message))
                {
                    //Outbox exception scenario
                    if (nhibernetGenericADOException.Message.Contains("could not insert: [NServiceBus.Outbox.NHibernate.OutboxRecord]"))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

    }
}
