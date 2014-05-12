namespace SelfHosingConsoleHost
{
    using System;
    using NServiceBus;
    using NServiceBus.Installation.Environments;

    class Program
    {
        static void Main(string[] args)
        {
            Configure.Features.Disable<NServiceBus.Features.Gateway>();
            Configure.Features.Disable<NServiceBus.Features.Sagas>();
            
            IBus bus = Configure.With()
                .DefineEndpointName("TEST4")
                .DefaultBuilder()
                .DisableGateway()
                .DisableTimeoutManager()
                .UseTransport<Msmq>().PurgeOnStartup(false)
                .UnicastBus().ImpersonateSender(false)
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());

            bus.SendLocal<TestMessage>(n => new TestMessage { Id = Guid.NewGuid() });

            while (Console.ReadLine() != null)
            {
            }
        }
    }
}
