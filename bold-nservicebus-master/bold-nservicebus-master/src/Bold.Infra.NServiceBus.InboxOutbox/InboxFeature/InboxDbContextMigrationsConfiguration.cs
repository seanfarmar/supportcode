using System.Data.Entity.Migrations;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    internal sealed class InboxDbContextMigrationsConfiguration : DbMigrationsConfiguration<InboxDbContext>
    {
        public InboxDbContextMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
