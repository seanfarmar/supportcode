using System.Data.Entity.Migrations;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    internal sealed class InboxStartupDbContextConfiguration : DbMigrationsConfiguration<InboxStartupDbContext>
    {
        public InboxStartupDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}