namespace Server.Messages
{
    using System;
    using System.Collections.Generic;

    public class InternalTransactionControlMessage
    {
        public List<Guid> TransactionList { get; set; }

        public Guid CorrelationId { get; set; }        
    }
}