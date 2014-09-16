using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp
{
    public interface IMultipleImportMessage : IMessage
    {
        IFileImportMessage[] ImportMessages  { get; set; }
    }
}