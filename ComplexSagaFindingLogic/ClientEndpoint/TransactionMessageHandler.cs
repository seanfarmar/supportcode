namespace Client.Endpoint
{
    using System;
    using Messages;
    using NServiceBus;
    using Server.Messages;

    public class TransactionMessageHandler : IHandleMessages<TransactionMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(TransactionMessage message)
        {
            Console.WriteLine("Handling TransactionMessage with TransactionId {0} and {1}", message.TransactionId, message.CorrelationId);

            //Console.WriteLine("Doing Bus.Reply with ReplayMessage with TransactionId {0}", message.TransactionId);

            //Bus.Reply<ReplayMessage>(m => { m.TransactionId = message.TransactionId; });

            // go look in the saga persistance db

            Console.WriteLine("Doing Bus.Send with ReplayMessage with TransactionId {0}", message.TransactionId);

            Bus.Send<ReplayMessage>(m => { m.TransactionId = message.TransactionId; m.Id = Guid.NewGuid();});
        }
    }
}