namespace ErrorUOW.ErrorUnitOfWork
{
    using NServiceBus;

    public class UoWInitializer : INeedInitialization
    {
        public void Init()
        {
            Configure.Instance.Configurer.
                ConfigureComponent<ErrorDetectionUnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork);
        }
    }
}