using System;
using System.Threading;
using Microsoft.WindowsAzure;
using NServiceBus;
using NServiceBus.Features;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class InboxStartup : Feature
    {
        public InboxStartup()
        {
            DependsOn<Inbox>();
            RegisterStartupTask<InboxStartupTask>();
        }

        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Container.ConfigureComponent<InboxStartupTask>(DependencyLifecycle.SingleInstance)
                             .ConfigureProperty(t => t.TimeToKeepDeduplicationData, GetTimeToKeepDeduplicationData(context));
        }

        private static TimeSpan GetTimeToKeepDeduplicationData(FeatureConfigurationContext context)
        {
            TimeSpan dt;
            return context.Settings.TryGet("Outbox.TimeToKeepDeduplicationEntries", out dt) ? dt : TimeSpan.FromDays(7);
        }

        public sealed class InboxStartupTask : FeatureStartupTask, IDisposable
        {
            private Timer cleanupTimer;
            private readonly string connectionString = CloudConfigurationManager.GetSetting("Bold.DatabaseConnectionString");
            private readonly string schemaName = CloudConfigurationManager.GetSetting("NServiceBus/Persistence/NHibernate/default_schema");
            private InboxSettings inboxSettings;

            public TimeSpan TimeToKeepDeduplicationData { get; set; }

            public void Dispose()
            {
                cleanupTimer.Dispose();
            }

            protected override void OnStart()
            {
                inboxSettings = new InboxSettings(schemaName);
                cleanupTimer = new Timer(PerformCleanup, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            }

            protected override void OnStop()
            {
                using (var waitHandle = new ManualResetEvent(false))
                {
                    cleanupTimer.Dispose(waitHandle);

                    waitHandle.WaitOne();
                }
            }
            
            private void PerformCleanup(object state)
            {
                try
                {
                    using (var startupDbContext = new InboxStartupDbContext(connectionString, inboxSettings))
                    {
                        startupDbContext.RemoveEntriesOlderThan(DateTime.UtcNow - TimeToKeepDeduplicationData);
                    }
                }
                catch (Exception exception)
                {
                   throw new Exception("Error when trying to remove old entries from Inbox", exception);
                }
            }
        }
    }
}
