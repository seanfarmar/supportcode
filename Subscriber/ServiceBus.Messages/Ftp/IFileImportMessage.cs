namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    using NServiceBus;

    public interface IFileImportMessage : IMessage
    {
        string FileName { get; set; }
        string OutgoingDirectory { get; set; }
    }
}