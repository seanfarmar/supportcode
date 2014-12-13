namespace Tests.Saga
{
    using NServiceBus.Saga;

    public class AppTypePolicyData : ContainSagaData
    {
        public AppTypePolicyData()
        {
            AppType = Messages.AppType.Unknown;
        }

        [Unique]
        public int AppId { get; set; }

        public Messages.AppType AppType { get; set; }
    }
}