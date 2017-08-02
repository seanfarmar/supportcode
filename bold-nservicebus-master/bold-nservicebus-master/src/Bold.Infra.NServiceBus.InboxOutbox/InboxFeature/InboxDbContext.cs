using System.Data;
using System.Data.Common;
using System.Data.Entity;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class InboxDbContext : DbContext
    {
        public InboxDbContext()
        {
            TurnOffAutomaticDatabaseCreationAndSchemaUpdates();
        }

        public InboxDbContext(IDbConnection connection, IInboxSettings settings)
            : base((DbConnection)connection, false)
        {
            TurnOffAutomaticDatabaseCreationAndSchemaUpdates();
            Settings = settings;
        }

        protected InboxDbContext(string connectionString, IInboxSettings settings)
            : base(connectionString)
        {
            TurnOffAutomaticDatabaseCreationAndSchemaUpdates();
            Settings = settings;
        }

        public DbSet<InboxMessage> InboxRecords { get; set; }

        public IInboxSettings Settings { get; private set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (Settings != null)
            {
                modelBuilder.HasDefaultSchema(Settings.SchemaName);
            }

            base.OnModelCreating(modelBuilder);
        }

        private static void TurnOffAutomaticDatabaseCreationAndSchemaUpdates()
        {
            Database.SetInitializer<InboxDbContext>(null);
        }
    }
}