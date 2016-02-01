namespace Swiftness.Test.Common
{
    using System;
    using System.Collections.Generic;
    using NServiceBus.Logging;

    public interface IProcessContext
    {
        long GeneralFileDataRootKey { get; set; }

        DateTime CreatedDateTime { get; set; }

        String User { get; set; }

        String FileName { get; set; }

        String Account { get; set; }

        String SwiftNessKey { get; set; }

        String OutGoingFeedbackFileName { get; set; }

        String ErrorFeedbackFileName { get; set; }

        Boolean IsFeedBackByFile { get; set; }

        String ApplicationDBConnectionString { get; set; }
        int ApplicationDBRecordKey { get; set; }



        Boolean IsB2BAccount { get; set; }

        Boolean IsProductivityEvent { get; set; }

        long EventSourceAccountKey { get; set; }

        Boolean IsRecordLevel { get; set; }

        long RecordID { get; set; }

        Guid SessionId { get; set; }

        string ProductType { get; set; }

        int NumberOfRecords { get; set; }
        Boolean IsInfoVault { get; set; }
    }

    public enum MonitorEventIdEnum
    {
        Default,
        FileLevelErrorMessageFromCustomer = 2420,
        UnknownSwiftnessKey = 2423,
        RecordLevelErrorMessageFromCustomer = 2424,
        InvalidFileFromPortal = 2425,
        CustomerThresholdExceeded = 2427,
        NotificationFailed = 2121,
        InvoiceFileProcessFailed = 2122,
        FeedbackBOutValidationError = 2431,
        ErrorCreatingFeedbackBOut = 2432
    }

    public interface ILoggerService2
    {
        IProcessContext ThreadContext { get; set; }

        void Debug(string message, LoggerEnum logger = LoggerEnum.Default);
        void Info(string message, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null);
        void Warn(string message, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null);
        void Error(string message, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null);
        void Error(Exception ex, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null);
        void Fatal(string message, LoggerEnum logger = LoggerEnum.Default);
        void Fatal(Exception ex, LoggerEnum logger = LoggerEnum.Default);
    }

    public enum LoggerEnum
    {
        Default,
        MonitorEvent
    }
    
    public class CoreLog4NetLoggerService : ILoggerService2
    {
        private static Dictionary<string, ILog> loggerMap;
        private readonly static object syncObject = new object();

        public CoreLog4NetLoggerService()
        {
            initLoggers();
        }

        #region Info
        public void Info(string message, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null)
        {
            log4net.ThreadContext.Properties["StackTrace"] = Environment.StackTrace;
            if (eventId.HasValue)
                log4net.ThreadContext.Properties["EventID"] = (int)eventId.Value;
            loggerMap[logger.ToString()].Info(message);
        }
        #endregion Info

        #region warn

        public void Warn(string message, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null)
        {
            log4net.ThreadContext.Properties["StackTrace"] = Environment.StackTrace;
            if (eventId.HasValue)
                log4net.ThreadContext.Properties["EventID"] = (int)eventId.Value;
            loggerMap[logger.ToString()].Warn(message);
        }

        #endregion

        #region debug

        public void Debug(string message, LoggerEnum logger = LoggerEnum.Default)
        {
            log4net.ThreadContext.Properties["StackTrace"] = Environment.StackTrace;
            loggerMap[logger.ToString()].Debug(message);
        }

        #endregion debug

        #region error

        public void Error(string message, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null)
        {
            log4net.ThreadContext.Properties["StackTrace"] = Environment.StackTrace;
            if (eventId.HasValue)
                log4net.ThreadContext.Properties["EventID"] = (int)eventId.Value;
            loggerMap[logger.ToString()].Error(message);
        }

        public void Error(Exception ex, LoggerEnum logger = LoggerEnum.Default, MonitorEventIdEnum? eventId = null)
        {
            string message = ex.ToString();
            Error(message, logger, eventId);
        }

        #endregion error

        #region fatal

        public void Fatal(string message, LoggerEnum logger = LoggerEnum.Default)
        {
            log4net.ThreadContext.Properties["StackTrace"] = Environment.StackTrace;
            loggerMap[logger.ToString()].Fatal(message);
        }

        public void Fatal(Exception ex, LoggerEnum logger = LoggerEnum.Default)
        {
            string message = ex.ToString();
            Fatal(message, logger);
        }

        #endregion fatal

        public IProcessContext ThreadContext
        {
            get
            {
                return (IProcessContext)log4net.ThreadContext.Properties["ProcessContext"];
            }
            set
            {
                log4net.ThreadContext.Properties["ProcessContext"] = value;
            }
        }

        private void initLoggers()
        {
            lock (syncObject)
            {
                if (loggerMap == null)
                {
                    string loggerName = string.Empty;
                    loggerMap = new Dictionary<string, ILog>();
                    foreach (LoggerEnum logger in Enum.GetValues(typeof(LoggerEnum)))
                    {
                        loggerName = logger.ToString();
                        ILog log = LogManager.GetLogger(loggerName);
                        loggerMap.Add(loggerName, log);
                    }
                }
            }
        }
    }
}
