using System;
using NServiceBus;
using PayrollGenerator.Messages;

namespace PayrollGenerator.Sender
{
    class ProcessOrderSender : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'Enter' to send a message. To exit, Ctrl + C");
            var counter = 0;
            
            while (Console.ReadLine() != null)
            {
                var payProcessStarter = new PayProcessStarter() { ProcessId = Guid.NewGuid() };

                Bus.Send(payProcessStarter);
            }
        }

        public void Stop()
        {

        }
    }
}
