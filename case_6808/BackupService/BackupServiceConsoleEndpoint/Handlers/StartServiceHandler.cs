namespace Slb.BackupServiceConsoleEndpoint.Handlers
{
    using System;

    using NServiceBus;

    using Slb.Messages;

    public class StartServiceHandler : IHandleMessages<IStartService>
    {
        public IBus Bus { get; set; }

        public void Handle(IStartService message)
        {
            Console.WriteLine("IStartService handled.");
        }
    }
}