namespace SelfHosingConsoleHost 
{
    using System;
    using NServiceBus;

    public class TestMessageHandler: IHandleMessages<TestMessage>
    {
        public void Handle(TestMessage message)
        {
            Console.WriteLine("Handeling TestMessage with id: {0}", message.Id);
        }
    }
}
