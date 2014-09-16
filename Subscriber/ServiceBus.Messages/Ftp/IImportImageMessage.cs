namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    using NServiceBus;

    public interface IImportImageMessage : IMessage
    {
        int? SourceId { get; set; }
    }
}