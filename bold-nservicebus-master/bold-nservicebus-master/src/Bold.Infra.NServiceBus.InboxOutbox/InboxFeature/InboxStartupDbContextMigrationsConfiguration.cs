using System.Data.Entity.Migrations;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    internal sealed class InboxStartupDbContextMigrationsConfiguration : DbMigrationsConfiguration<InboxStartupDbContext>
    {
        public InboxStartupDbContextMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}