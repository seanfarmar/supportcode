using NServiceBus;
using NServiceBus.Features;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class Inbox : Feature
    {
        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Pipeline.Register<InboxBehavior.InboxRegistration>();

            context.Container.ConfigureComponent<InboxDbContext>(DependencyLifecycle.InstancePerUnitOfWork);
            context.Container.ConfigureComponent<InboxRepository>(DependencyLifecycle.InstancePerUnitOfWork);
        }
    }
}
