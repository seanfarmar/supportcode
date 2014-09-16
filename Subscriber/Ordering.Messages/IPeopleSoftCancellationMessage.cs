namespace Ordering.Messages
{
    using NServiceBus;

    public interface IPeopleSoftCancellationMessage : IMessage
    {
        OrderCancellation[] Cancellations { get; set; }
    }
}