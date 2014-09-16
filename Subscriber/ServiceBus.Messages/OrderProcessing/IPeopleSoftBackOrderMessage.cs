using NServiceBus;
using Ordering.Messages;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IPeopleSoftBackOrderMessage : IMessage
    {
        BackOrder[] BackOrders { get; set; }
    }
}
