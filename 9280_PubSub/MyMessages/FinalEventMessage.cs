namespace MyMessages
{
    using System;

    [Serializable]
    public class FinalEventMessage : IMyEvent
    {
        public Guid EventId { get; set; }
        public DateTime? Time { get; set; }
        public TimeSpan Duration { get; set; }
    }
}