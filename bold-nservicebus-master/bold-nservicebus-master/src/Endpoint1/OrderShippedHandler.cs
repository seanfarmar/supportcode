using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Endpoint1
{
    public class OrderShippedHandler :
        IHandleMessages<OrderShippedEvent>
    {
        static ILog log = LogManager.GetLogger<OrderShippedHandler>();
        public void Handle(OrderShippedEvent message)
        {
            log.Info($"Received OrderShippedEvent: {message.ShippingDate}");
        }
    }
}