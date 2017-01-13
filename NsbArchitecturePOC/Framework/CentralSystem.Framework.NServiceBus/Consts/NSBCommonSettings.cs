namespace CentralSystem.Framework.NServiceBus.Consts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Common constants
    /// </summary>
    public static class NSBCommonSettings
    {

        /// <summary>
        /// Endpoint name.
        /// Initialized by Bus Configuration extension method SetApplicationNameAndEndpointName.
        /// </summary>
        public static string EndpointName
        {
            get;
            internal set;
        }

        /// <summary>
        /// NServiceBus headers
        /// </summary>
        internal static class Headers
        {
            /// <summary>
            /// Exception info type for implementation of second level retry policy
            /// </summary>
            public const string NSB_EXCEPTION_INFO_TYPE = "NServiceBus.ExceptionInfo.ExceptionType";

            /// <summary>
            /// Exception info full stack trace for implementation of second level retry policy
            /// </summary>
            public const string NSB_EXCEPTION_INFO_STACK_TRACE = "NServiceBus.ExceptionInfo.StackTrace";
        }

        /// <summary>
        /// Default NSB log message code
        /// </summary>
        public const int NSERVICE_BUS_LOGGER_DEFAULT_CODE = 10;

        /// <summary>
        /// Default NSB fatal error code for critical error
        /// </summary>
        public const int INT_NSB_FATAL_ERROR_CODE_WITHOUT_SHOUT_DOWN = 13;

        /// <summary>
        /// Default NSB fatal error code for critical error with shut down
        /// </summary>
        public const int INT_NSB_FATAL_ERROR_CODE_WITH_PROCESS_SHUT_DOWN = 14;

        /// <summary>
        /// Default NSB optimistic error code
        /// </summary>
        public const int INT_NSB_OPTIMISTIC_LOCK_ERROR_CODE = 30;

        /// <summary>
        /// Default NSB fatal error message for critical error
        /// </summary>
        internal const string INT_NSB_FATAL_ERROR_MESSAGE = "The following critical error was encountered by NSB endpoint \"{0}\":\n{1}";

        /// <summary>
        /// Default NSB fatal error message for critical error - extended message
        /// </summary>
        internal const string INT_NSB_FATAL_ERROR_MESSAGE_EXTENDED_SERVICE_SHUTTING_DOWN = "NSB service is shutting down.";

        /// <summary>
        /// Default NSB fatal error message for critical error - extended message
        /// </summary>
        internal const string INT_NSB_FATAL_ERROR_MESSAGE_EXTENDED_SERVICE_IN_RECOVERY_MODE = "NSB service is in recovery mode.";

        /// <summary>
        /// Application setting name for SLA threshold
        /// </summary>
        internal const string NSB_SLA_THRESHOLD_APP_SETTING_NAME = "NServiceBus/SLAThreshold";

        /// <summary>
        /// Application setting name for Outbox feature
        /// </summary>
        internal const string NSB_OUTBOX_FEATURE_ENABLE_SETTING_NAME = "NServiceBus/Outbox";

        /// <summary>
        /// Min and default threshold time interval for configuration of NServiceBus performance counter "SLA violation countdown"
        /// </summary>
        internal static readonly TimeSpan MIN_AND_DEFAULT_ENDPOINT_SLA_THRESHOLD_TIME_INTERVAL = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Connection string name
        /// </summary>
        public const string SQL_CONNECTION_STRING_NAME = "WorkFlows";

        /// <summary>
        /// Default error queue
        /// </summary>
        public const string DEFAULT_ERROR_QUEUE_NAME = "Errors";

        /// <summary>
        /// The maximum number of times to retry processing a message when it fails before
        /// moving it to the error queue.
        /// By default transport retry disabled and enabled second level.
        /// </summary>
        public const int DEFAULT_MAX_RETRIES = 1;

        /// <summary>
        /// Specifies the the parameter name for maximum concurrency level.
        /// </summary>
        internal const string MAXIMUM_CONCURRENCY_LEVELPARAM_APP_SETTING_NAME = "NServiceBus/MaximumConcurrencyLevel";

        /// <summary>
        /// Specifies the maximum concurrency level.
        /// For increasing of concurrency - use transport configuration in the specific application package.
        /// </summary>
        public const int DEFAULT_MAXIMUM_CONCURRENCY_LEVEL = 2;

        /// <summary>
        /// The max throughput for the transport. This allows the user to throttle their
        /// endpoint if needed
        /// </summary>
        public const int DEFAULT_MAXIMUM_MESSAGE_THROUGHPUT_PER_SECOND = 0;

        /// <summary>
        /// Default time to wait before generate critical error
        /// </summary>
        internal static readonly TimeSpan DEFAULT_CRITICAL_ERROR_TIME_TO_WAIT = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Built-in NServiceBus performance counters category name
        /// </summary>
        internal static string NSERVICEBUS_BUILTIN_PERFORMANCE_COUNTERS_CATEGORY_NAME = "NServiceBus";

        /// <summary>
        /// Custom CGS EndPoints NServiceBus performance counters category name
        /// </summary>
        internal static string NSERVICEBUS_CGS_ENDPOINTS_PERFORMANCE_COUNTERS_CATEGORY_NAME = "CGS:NSB_EndPoints";

        /// <summary>
        /// Custom CGS EndPoint NServiceBus performance counter name
        /// </summary>
        internal static string NSERVICEBUS_CGS_ENDPOINT_IS_ACTIVE_PERFORMANCE_COUNTER_NAME = "End Point is Active";

        /// <summary>
        /// Custom CGS EndPoint NServiceBus performance counter name
        /// </summary>
        internal static string NSERVICEBUS_CGS_ENDPOINT_TOTAL_MESSAGES_STARTED_PROCESSING_COUNTER_NAME = "Total Messages Started Processing";

        /// <summary>
        /// Custom CGS EndPoint NServiceBus performance counter name
        /// </summary>
        internal static string NSERVICEBUS_CGS_ENDPOINT_MESSAGES_IN_PROCESS_PERFORMANCE_COUNTER_NAME = "Messages in Process";

        /// <summary>
        /// Custom CGS EndPoint NServiceBus performance counter name
        /// </summary>
        internal static string NSERVICEBUS_CGS_ENDPOINT_TOTAL_MESSAGES_COMPLETED_SUCCESSFULLY_COUNTER_NAME = "Total Messages Completed Processing Successfully";

        /// <summary>
        /// Custom CGS EndPoint NServiceBus performance counter name
        /// </summary>
        internal static string NSERVICEBUS_CGS_ENDPOINT_TOTAL_RETRY_ERRORS_COUNTER_NAME = "Total NSB Retry Errors";

    }
}
