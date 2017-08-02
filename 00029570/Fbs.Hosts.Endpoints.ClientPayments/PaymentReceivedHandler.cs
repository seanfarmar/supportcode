namespace Fbs.Hosts.Endpoints.ClientPayments
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Messages.Commands;
    using NServiceBus;
    
    class PaymentReceivedHandler : IHandleMessages<PaymentReceived>
    {
        public Task Handle(PaymentReceived message, IMessageHandlerContext context)
        {
            Console.WriteLine("Handeling PaymentReceived message with Id: {0}", message.Id);

            return Task.CompletedTask;
        }
    }
}
