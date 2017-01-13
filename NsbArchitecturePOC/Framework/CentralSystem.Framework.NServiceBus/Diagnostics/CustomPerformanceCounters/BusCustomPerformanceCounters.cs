namespace CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.Exceptions;
    using CentralSystem.Framework.NServiceBus.Consts;

    /// <summary>
    /// Bus custom performance counters definitions.
    /// 
    /// Pattern for definition of new performance counter:
    /// - Public property creator function with private setter
    /// - In the Initialize append initialization of performance counter
    /// - Implementation class before activation also should check if property was initialized. 
    ///   This is indication of optional performance counter feature.
    ///   
    /// The reason to save static functions to activate performance counter instance is using of "Increment"/"Decrement" functions.
    /// From MSDN - https://msdn.microsoft.com/en-us/library/system.diagnostics.performancecounter.decrement(v=vs.110).aspx:
    /// In multithreaded scenarios, some updates to the counter value might be ignored, resulting in inaccurate data.
    /// </summary>
    internal static class BusCustomPerformanceCounters
    {

        #region Members

        /// <summary>
        /// Logger
        /// </summary>
        private readonly static global::NServiceBus.Logging.ILog s_logger = global::NServiceBus.Logging.LogManager.GetLogger(typeof(BusCustomPerformanceCounters));

        /// <summary>
        /// Performance counter - NServiceBus active endpoint flag.
        /// Reset counter from critical error action handler.
        /// Restore counter - on any received message from queue.
        /// </summary>
        public static Func<PerformanceCounter> EndpointIsActiveCreator { get; private set; }

        /// <summary>
        /// Performance counter - NServiceBus total started messages
        /// </summary>
        public static Func<PerformanceCounter> TotalMessagesStartedProcessingCreator { get; private set; }

        /// <summary>
        /// Performance counter - NServiceBus messages concurrency handling
        /// </summary>
        public static Func<PerformanceCounter> MessagesInProgressCreator { get; private set; }

        /// <summary>
        /// Performance counter - NServiceBus total success messages
        /// </summary>
        public static Func<PerformanceCounter> TotalMessagesCompletedSuccessfullyCreator { get; private set; }

        /// <summary>
        /// Performance counter - NServiceBus total errors (excluded optimistic lock exception)
        /// </summary>
        public static Func<PerformanceCounter> TotalRetryErrorsCreator { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize all relevant performance counters if registered
        /// </summary>
        public static void InitializeCreators()
        {
            EndpointIsActiveCreator = InitializePerformanceCounterCreatorIfExists(
                NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINT_IS_ACTIVE_PERFORMANCE_COUNTER_NAME, System.Int32.MaxValue);

            TotalMessagesStartedProcessingCreator = InitializePerformanceCounterCreatorIfExists(
                NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINT_TOTAL_MESSAGES_STARTED_PROCESSING_COUNTER_NAME, 0);

            MessagesInProgressCreator = InitializePerformanceCounterCreatorIfExists(
                NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINT_MESSAGES_IN_PROCESS_PERFORMANCE_COUNTER_NAME, 0);

            TotalMessagesCompletedSuccessfullyCreator = InitializePerformanceCounterCreatorIfExists(
                NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINT_TOTAL_MESSAGES_COMPLETED_SUCCESSFULLY_COUNTER_NAME, 0);

            TotalRetryErrorsCreator = InitializePerformanceCounterCreatorIfExists(
                NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINT_TOTAL_RETRY_ERRORS_COUNTER_NAME, 0);
        }

        /// <summary>
        /// Helper function to initialize performance counter
        /// </summary>
        /// <param name="counterName">Counter name</param>
        /// <param name="initValue">Optional initialization value</param>
        /// <returns>Performance counter creator function</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static Func<PerformanceCounter> InitializePerformanceCounterCreatorIfExists(string counterName, long initValue)
        {
            Func<PerformanceCounter> resultPerformanceCounterCreator = null;

            try
            {
                resultPerformanceCounterCreator = () => 
                    {
                        PerformanceCounter performanceCounterToWrite = new PerformanceCounter();

                        performanceCounterToWrite.CategoryName = NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINTS_PERFORMANCE_COUNTERS_CATEGORY_NAME;
                        performanceCounterToWrite.CounterName = counterName;
                        performanceCounterToWrite.InstanceName = NSBCommonSettings.EndpointName;
                        performanceCounterToWrite.ReadOnly = false;

                        return performanceCounterToWrite;

                    };

                //Init value with check performance counter activation
                resultPerformanceCounterCreator().RawValue = initValue;

            }
            catch (Exception ex)
            {
                s_logger.Fatal(string.Format("Error on attempt to initiate performance counter names '{0}' related to category '{1}'",
                        counterName, NSBCommonSettings.NSERVICEBUS_CGS_ENDPOINTS_PERFORMANCE_COUNTERS_CATEGORY_NAME), 
                    ex);

                //In case of any error current counter will not work in the current process
                resultPerformanceCounterCreator = null;

            }

            return resultPerformanceCounterCreator;
        }

        #endregion

    }
}
