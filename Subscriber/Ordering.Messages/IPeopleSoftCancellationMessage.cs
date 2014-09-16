using Hinda.Internal.ServiceBus.Messages.OrderProcessing;
using NServiceBus;

namespace Ordering.Messages
{
    public interface IPeopleSoftCancellationMessage : IMessage
    {
        OrderCancellation[] Cancellations { get; set; }
    }
}
