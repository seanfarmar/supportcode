namespace Tests.Handler
{
    using System;
    using Messages;
    using NServiceBus;

    class AppTypeResponseHandler : IHandleMessages<AppTypeResponse>
    {
        public void Handle(AppTypeResponse message)
        {
            Console.WriteLine("Handling AppTypeResponse AppType should be unknown and is: {0}", message.AppType);
        }
    }
}
