namespace Tests.Saga
{
    using Messages;
    using NServiceBus.Saga;

    public class AppTypePolicy : Saga<AppTypePolicyData>, 
        IAmStartedByMessages<AppTypeRequest>,
        IAmStartedByMessages<SetAppType>
    {
        public void Handle(AppTypeRequest message)
        {
            Data.AppId = message.AppId;

            var response = new AppTypeResponse {AppId = Data.AppId, AppType = Data.AppType};
            Bus.Reply(response);
        }

        public void Handle(SetAppType message)
        {
            Data.AppId = message.AppId;
            Data.AppType = AppType.Known;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AppTypePolicyData> mapper)
        {
            mapper.ConfigureMapping<SetAppType>(m => m.AppId).ToSaga(s => s.AppId);

            mapper.ConfigureMapping<AppTypeRequest>(m=>m.AppId).ToSaga(s => s.AppId);
        }
    }
}