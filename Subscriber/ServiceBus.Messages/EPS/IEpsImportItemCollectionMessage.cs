using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    public interface IEpsImportItemCollectionMessage : IMessage
    {
        int ItemCollectionId{ get; set; }        
    }
}