using System;

namespace IntegrationGateWay.Sender
{
    using Messages;
    using NServiceBus;

    public class Application : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'S' to send a message that will say Hallo");
            Console.WriteLine("Press 'Q' to exit.");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                switch (cmd)
                {
                    case "s":

                        Bus.Send<SendSomething>(m =>
                        {
                            m.SomeData = "Hallo World!";
                            m.Id = Guid.NewGuid();
                        });
                        
                        break;                    
                }
            }
        }

        public void Stop()
        {
        }
    }
}
