namespace Fbs.Contracts.Messages.Commands
{
    using NServiceBus;

    public class PaymentReceived : ICommand
    {
        public string Id { get; set; }
    }
}