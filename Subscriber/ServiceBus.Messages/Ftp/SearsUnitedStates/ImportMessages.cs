using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.Ftp.SearsUnitedStates
{
    public interface IProductMessage : ISearsImportMessage { }
    public interface IPriceMessage : ISearsImportMessage { }
    public interface ICategoryMessage : ISearsImportMessage { }
    public interface IOrderFeedMessage : ISearsImportMessage { }
    public interface IAvailabilityMessage : ISearsImportMessage { }

    public interface ISearsImportMessage :  IMessage
    {
        IFileImportMessage[] ImportMessages { get; set; }
    }
}
