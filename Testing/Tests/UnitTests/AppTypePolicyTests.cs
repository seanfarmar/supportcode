namespace Tests.UnitTests
{
    using Messages;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Saga;

    [TestFixture]
    public class AppTypePolicyTests
    {
        [Test]
        public void RequestWithNoSetEventShouldReturnUnknown()
        {
            var request = new AppTypeRequest {AppId = 111};

            Test.Initialize();
            Test.Saga<AppTypePolicy>()
                .When(r => r.Handle(request))
                .ExpectReply<AppTypeResponse>(r => r.AppType == AppType.Unknown);
        }

        [Test]
        public void RequestShouldReturnKnownIfApplicationReceivedEventReceivedFirst()
        {
            var request = new AppTypeRequest {AppId = 111};
            var appReceivedEvent = new SetAppType {AppId = 111};

            Test.Initialize();
            Test.Saga<AppTypePolicy>()
                .When(r => r.Handle(appReceivedEvent))
                .When(r => r.Handle(request))
                .ExpectReply<AppTypeResponse>(r => r.AppType == AppType.Known);
        }

        [Test]
        public void RequestShouldReturnUnknownAsSetAndRequestEventAppIdsDoNotMatch()
        {
            var appReceivedEvent = new SetAppType {AppId = 222};
            var request = new AppTypeRequest {AppId = 111};
            
            Test.Initialize();
            Test.Saga<AppTypePolicy>()
                .When(r => r.Handle(appReceivedEvent))
                .When(r => r.Handle(request))
                .ExpectReply<AppTypeResponse>(r => r.AppType == AppType.Unknown);
        }
    }
}