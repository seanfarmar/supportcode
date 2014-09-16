namespace Ordering.Messages
{
    using Hinda.Internal.ServiceBus.Messages.OrderProcessing;
    using NServiceBus;

    public interface IPeopleSoftResponseMessage : IMessage
    {
        PeopleSoftOrder Order { get; set; }
    }
}