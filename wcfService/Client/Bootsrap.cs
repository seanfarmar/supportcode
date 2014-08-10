namespace Client
{
    using System;
    using NServiceBus;
    using ServiceReference1;

    public class Bootsrap : IWantToRunWhenBusStartsAndStops
    {
        public void Start()
        {
            Console.WriteLine("Press 's' to send lots of commands");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
                {
                    case "s":

                        Console.WriteLine("==========================================================================");

                        var proxy = new CreateProductServiceClient();

                        var guid = Guid.NewGuid();

                        var message = new CreateProductMessage
                        {
                            GuidId = guid,
                            ProductNumber = DateTime.Now.Second,
                            Name = "ProductName" + guid
                        };

                        var response = proxy.Create(new CreateRequest(message)).CreateResult;

                        Console.WriteLine("create invoked, response: {0}", response);

                        break;
                }
            }
        }

        public void Stop()
        {
        }
    }
}