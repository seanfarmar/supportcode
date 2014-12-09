namespace PayrollGenerator.Sender
{
    using System;
    using Messages.Commands;
    using NServiceBus;

    internal class ProcessOrderSender : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to send a message. To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
                var payProcessStarter = new PayProcessStarter {ProcessId = Guid.NewGuid()};

                Bus.Send(payProcessStarter);
            }
        }

        public void Stop()
        {
        }
    }
}