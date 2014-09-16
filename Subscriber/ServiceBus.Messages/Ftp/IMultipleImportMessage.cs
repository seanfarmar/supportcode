namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    using NServiceBus;

    public interface IMultipleImportMessage : IMessage
    {
        IFileImportMessage[] ImportMessages { get; set; }
    }
}