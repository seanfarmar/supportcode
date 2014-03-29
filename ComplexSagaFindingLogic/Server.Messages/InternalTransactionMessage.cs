namespace Server.Messages
{
    using System;

    public class InternalTransactionMessage
    {
        public Guid TransactionId { get; set; }

        public Guid CorrelationId { get; set; }        
    }
}