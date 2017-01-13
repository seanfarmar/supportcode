namespace CentralSystem.Framework.NServiceBus.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using CentralSystem.Framework.Exceptions;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Diagnostics;
    using CustomPerformanceCounters = CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters;

    /// <summary>
    /// Default critical error bus action.
    /// Registration should be executed by BusConfigurationExtensions.
    /// </summary>
    internal sealed class BusCriticalErrorDefaultAction
    {

        #region Members

        /// <summary>
        /// True - continue to crash process on any critical error
        /// </summary>
        private readonly bool m_forceToCrashProcessOnAnyCriticalError = false;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly static global::NServiceBus.Logging.ILog s_logger = global::NServiceBus.Logging.LogManager.GetLogger(typeof(BusCriticalErrorDefaultAction));

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with all settings
        /// </summary>
        /// <param name="forceToCrashProcessOnAnyCriticalError">True - crash process on any critical error</param>
        public BusCriticalErrorDefaultAction(bool forceToCrashProcessOnAnyCriticalError)
        {
            m_forceToCrashProcessOnAnyCriticalError = forceToCrashProcessOnAnyCriticalError;
        }

        #endregion

        #region Members

        /// <summary>
        /// Critical error event
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <param name="exception">Exception</param>
        public void OnCriticalError(string errorMessage, Exception exception)
        {
            bool crashProcess = m_forceToCrashProcessOnAnyCriticalError || IsCriticalErrorToCrashProcess(exception);

            string fullErrorMessage = string.Format(NSBCommonSettings.INT_NSB_FATAL_ERROR_MESSAGE,
                NSBCommonSettings.EndpointName,
                errorMessage);
            if (crashProcess)
            {
                fullErrorMessage += "\n" + NSBCommonSettings.INT_NSB_FATAL_ERROR_MESSAGE_EXTENDED_SERVICE_SHUTTING_DOWN;
            }
            else
            {
                fullErrorMessage += "\n" + NSBCommonSettings.INT_NSB_FATAL_ERROR_MESSAGE_EXTENDED_SERVICE_IN_RECOVERY_MODE;
            }

            s_logger.Fatal(fullErrorMessage, exception);

            if (!crashProcess)
            {
                //Notify about endpoint inactive state
                CustomPerformanceCounters.BusReceiveStartedEventSubscriber.MarkEndPointAsInactive();

            }


            //For console DEBUG mode
            if (Environment.UserInteractive)
            {
                Console.WriteLine(fullErrorMessage);
                Thread.Sleep(10000);

            }

            // Crash the process on a critical error
            if (crashProcess)
            {
                //Try Give time to Logger to send FATAL error
                Thread.Sleep(1000);
                Environment.FailFast(fullErrorMessage, exception);

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determinate exception type for condition to shut down process.
        /// Recommendation of critical errors from NServiceBus - http://docs.particular.net/nservicebus/errors/exception-caveats.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>True - in any case to kill process</returns>
        private bool IsCriticalErrorToCrashProcess(Exception exception)
        {
            return (exception is StackOverflowException) 
                || (exception is OutOfMemoryException)
                || (exception is AccessViolationException);
        }

        #endregion

    }
}
