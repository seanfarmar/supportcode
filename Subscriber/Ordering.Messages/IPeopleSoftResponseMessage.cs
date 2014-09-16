using Hinda.Internal.ServiceBus.Messages.OrderProcessing;
using NServiceBus;

namespace Ordering.Messages
{
    public interface IPeopleSoftResponseMessage : IMessage
    {
        PeopleSoftOrder Order { get; set; }
    }
}