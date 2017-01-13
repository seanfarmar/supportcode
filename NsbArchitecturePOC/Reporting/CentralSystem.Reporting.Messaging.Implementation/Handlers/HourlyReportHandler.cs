namespace CentralSystem.Reporting.Messaging.Implementation.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow;
    using CentralSystem.Messaging.FlowManager;
    using CentralSystem.Messaging.FlowManager.DrawFlow;
    using CentralSystem.Messaging.Implementation.Handlers;
    using CentralSystem.Messaging.Implementation.Mapping;
    using CentralSystem.Messaging.Implementation.Mapping.FlowManager;
    using NServiceBus;

    public class HourlyReportHandler : BaseMessageHandler, IHandleMessages<GenerateHourlySignedTicketsCommand>
    {

        private readonly static global::NServiceBus.Logging.ILog s_logger = global::NServiceBus.Logging.LogManager.GetLogger(typeof(HourlyReportHandler));

        private static readonly Dictionary<int, string> s_lastMessageIDPerFlowInstanceID = new Dictionary<int, string>();

        /// <summary>
        /// Handles the Hourly report command
        /// </summary>
        /// <param name="message"></param>
        public void Handle(GenerateHourlySignedTicketsCommand message)
        {
            FlowActivityResultMessage activityResultMessage = new FlowActivityResultMessage();

            DrawFlowHourlyActivityCommand businessCommand = new DrawFlowHourlyActivityCommand();

            s_logger.Info("Arrived message ID: " + Bus.CurrentMessageContext.Id);

            lock (s_lastMessageIDPerFlowInstanceID)
            {
                string lastID = null;
                if (s_lastMessageIDPerFlowInstanceID.TryGetValue(message.FlowInstanceID, out lastID) && lastID == Bus.CurrentMessageContext.Id)
                {
                    s_logger.Error("Duplicated message ID: " + lastID);
                    System.Diagnostics.EventLog.WriteEntry("Application", "Duplicated message ID: " + lastID, System.Diagnostics.EventLogEntryType.Error, 1000);
                }
                s_lastMessageIDPerFlowInstanceID[message.FlowInstanceID] = Bus.CurrentMessageContext.Id;
            }

            new FlowActivityCommandMappingBuilder().ExecuteMapping(message, activityResultMessage);

            Bus.SendResultMessageWithIdempotencyVerification(activityResultMessage, businessCommand.RequestContext);

        }

    }
}
