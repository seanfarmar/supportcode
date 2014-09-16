using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IPeopleSoftCancellationMessage : IMessage
    {
        OrderCancellation[] Cancellations { get; set; }
    }
}
