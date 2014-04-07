namespace Server.Saga
{
    using System;
    using System.Collections.Generic;
    using Messages;
    using NServiceBus;

    public class Starter: IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 's' to send a message");
            
            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                switch (cmd)
                {
                    case "s":

                        Console.WriteLine("=========================");

                        Console.WriteLine("Sending a new batch...");

                        var correlationId = Guid.NewGuid();

                        var control = new InternalTransactionControlMessage{CorrelationId = correlationId, TransactionList = new List<Guid>()};

                        var t1 = Guid.NewGuid();

                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = t1;
                        });

                        control.TransactionList.Add(t1);

                        var t2 = Guid.NewGuid();

                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = t2;
                        });

                        control.TransactionList.Add(t2);

                        var t3 = Guid.NewGuid();
                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = t3;
                        });

                        control.TransactionList.Add(t3);

                        var t4 = Guid.NewGuid();
                        Bus.SendLocal<InternalTransactionMessage>(m =>
                        {
                            m.CorrelationId = correlationId;
                            m.TransactionId = t4;
                        });

                        control.TransactionList.Add(t4);

                        Bus.SendLocal(control);

                        break;
                }                
            }
        }

        public void Stop()
        {
        }
    }
}