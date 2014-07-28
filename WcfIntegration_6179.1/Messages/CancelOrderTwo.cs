using NServiceBus;

namespace Messages
{
    public class CancelOrderTwo : IMessage
    {
        public int OrderId { get; set; }
    }
}