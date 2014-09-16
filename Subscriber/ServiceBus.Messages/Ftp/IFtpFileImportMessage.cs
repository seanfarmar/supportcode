namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    public interface IFtpFileImportMessage : IFileImportMessage
    {
        string OriginalLocation { get; set; }
    }
}