using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.OrderProcessing
{
    public interface IPeopleSoftResponseMessage : IMessage
    {
        PeopleSoftOrder Order { get; set; }
    }
}