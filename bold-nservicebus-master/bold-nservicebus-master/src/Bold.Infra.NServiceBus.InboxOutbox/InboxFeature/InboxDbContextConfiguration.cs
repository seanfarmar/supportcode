using System.Data.Entity.Migrations;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    internal sealed class InboxDbContextConfiguration : DbMigrationsConfiguration<InboxDbContext>
    {
        public InboxDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}