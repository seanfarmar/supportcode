namespace Server.Messages
{
    using System;

    public class TransactionMessage
    {
        public int TransactionId { get; set; }

        public Guid CorrelationId { get; set; }        
    }
}