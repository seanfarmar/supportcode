using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    public interface IFileImportMessage : IMessage
    {
        string FileName { get; set; }
        string OutgoingDirectory { get; set; }
    }
}