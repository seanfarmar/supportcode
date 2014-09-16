namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    using NServiceBus;

    public interface IEpsImportItemCollectionMessage : IMessage
    {
        int ItemCollectionId { get; set; }
    }
}