using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    public interface IImportImageMessage : IMessage
    {
        int? SourceId { get; set; }
    }
}