using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp.IngramBooks
{
    public interface IProductDeltaMessage : IIngramBooksMessage { }
    public interface IInventoryDeltaMessage : IIngramBooksMessage { }
    public interface IProductExclusionMessage : IIngramBooksMessage { }
    public interface IAnnotationMessage : IIngramBooksMessage { }
    public interface IDesirabilityMessage : IIngramBooksMessage { }
    public interface ICategoryMessage : IMultipleImportMessage { }

    public interface IFullImportMessage : IFileImportMessage
    {
        IFileImportMessage ProductMessage { get; set; }
        IFileImportMessage InventoryMessage { get; set; }
    }

    public interface IImagesImportMessage : IMessage
    {
        string DeltaImagesDirectory { get; set; }
    }

    public interface IIngramBooksMessage : IMessage
    {
        IFileImportMessage ImportMessage { get; set; }
    }

    public interface IIngramBooksAnnotationMessage : IIngramBooksMessage
    {
        IngramBooksAnnotationSource AnnotationSource { get; set; }
    }

    public enum IngramBooksAnnotationSource
    {
        IngramBooks,
        PublisherMarketing
    }
}
