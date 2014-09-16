using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp.IngramEntertainment
{
    public interface IInventoryImportMessage : IIngramImportMessage { }
    public interface IProductImportMessage : IIngramImportMessage { }
    public interface IContentImportMessage : IIngramImportMessage { }

    public interface IIngramImportMessage : IMessage
    {
        IFileImportMessage[] ImportMessages  { get; set; }
    }

    public interface IImagesImportMessage : IMessage
    {
        string ImageFilePath { get; set; }
    }
}
