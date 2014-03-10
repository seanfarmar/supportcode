namespace Handlers
{
    using System;
    using Messages;
    using NServiceBus;

    public class TestMessageHandler : IHandleMessages<TestMessage>
    {
        public void Handle(TestMessage message)
        {
            Console.WriteLine("Handeling TestMessage");
        }
    }
}