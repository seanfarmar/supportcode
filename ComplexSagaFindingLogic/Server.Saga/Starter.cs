namespace Server.Saga
{
    using System;
    using Messages;
    using NServiceBus;

    public class Starter: IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 'S' to send a message");
            
            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                switch (cmd)
                {
                    case "s":
                        var correlationId = Guid.NewGuid();

                        const int startingTransactionId = 1000;

                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = startingTransactionId;
                        });

                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = startingTransactionId + 1;
                        });

                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = startingTransactionId + 2;
                        });

                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = startingTransactionId + 3;
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