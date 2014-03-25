namespace Server.Messages
{
    using System;

    public class InternalTransactionMessage
    {
        public int TransactionId { get; set; }

        public Guid CorrelationId { get; set; }        
    }
}