namespace Hinda.Internal.ServiceBus.Messages.Ftp.IngramEntertainment
{
    public interface IOrderFilesMessage : IFileImportMessage
    {
        IFtpFileImportMessage[] ShipmentNotificationMessages { get; set; }
        IFtpFileImportMessage[] InvoiceReceivedMessages { get; set; }
        IFtpFileImportMessage[] OrderAcknowledgementMessages { get; set; }
        IFtpFileImportMessage[] FunctionalAcknowledgementMessages { get; set; }
    }
}