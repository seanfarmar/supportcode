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
                        var msg = new SendSomething
                        {
                            SomeData = "Hallo World! " + Guid.NewGuid(),
                            Id = Guid.NewGuid()
                        };

                        Bus.Send(msg);
                        
                        Console.WriteLine("");

                        Console.WriteLine("Sending message with id: {0}", msg.Id);

                        break;                    
                }
            }
        }

        public void Stop()
        {
        }
    }
}
