namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using NServiceBus;

    public interface IPeopleSoftCancellationMessage : IMessage
    {
        OrderCancellation[] Cancellations { get; set; }
    }
}