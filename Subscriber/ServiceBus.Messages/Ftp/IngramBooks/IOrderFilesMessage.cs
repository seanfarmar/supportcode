using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp.IngramBooks
{
    public interface IOrderFilesMessage : IMessage
    {
        IFtpFileImportMessage[] ShipmentNotificationMessages { get; set; }
        IFtpFileImportMessage[] InvoiceReceivedMessages { get; set; }
        IFtpFileImportMessage[] OrderAcknowledgementMessages { get; set; }
        IFtpFileImportMessage[] FunctionalAcknowledgementMessages { get; set; }
    }
}
