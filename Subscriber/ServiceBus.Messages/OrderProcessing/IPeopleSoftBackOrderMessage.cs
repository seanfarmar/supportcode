namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using NServiceBus;
    using Ordering.Messages;

    public interface IPeopleSoftBackOrderMessage : IMessage
    {
        BackOrder[] BackOrders { get; set; }
    }
}