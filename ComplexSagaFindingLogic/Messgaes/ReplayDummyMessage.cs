namespace Client.Messages
{
    using System;

    public class ReplayDummyMessage
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; } 
    }
}