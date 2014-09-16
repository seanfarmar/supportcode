namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    using NServiceBus;

    public interface IAllocateStockMessage : IMessage
    {
        int ClientApplicationId { get; set; }
        int OrderId { get; set; }
    }
}