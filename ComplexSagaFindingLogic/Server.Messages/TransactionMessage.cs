namespace Server.Messages
{
    using System;

    public class TransactionMessage
    {
        public Guid TransactionId { get; set; }

        public Guid CorrelationId { get; set; }        
    }
}