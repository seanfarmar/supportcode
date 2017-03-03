using System;
using System.Threading;
using System.Transactions;
using Entities;
using NHibernate;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Endpoint2
{
    public class ShipOrderHandler : IHandleMessages<ShipOrderCommand>
    {
        IBus bus;
        static ILog log = LogManager.GetLogger<ShipOrderHandler>();
        private readonly ISession session;

        public ShipOrderHandler(IBus bus, ISession session)
        {
            this.session = session;
            this.bus = bus;
        }

        public void Handle(ShipOrderCommand message)
        {
            log.Info($"Received ShipOrderCommand: OrderId: {message.OrderId} ShippingDate: {message.ShippingDate}");
            var messageId = bus.GetMessageHeader(message, Headers.MessageId);
            log.Info($"MessageId: {messageId}");
            var transaction = Transaction.Current;
            var isolationLevel = transaction == null ? "null" : transaction.IsolationLevel.ToString();
            log.Warn($"IsolationLevel: {isolationLevel}"); //transaction is null if Outbox is NOT enabled
            string nhIsolationLevel = string.Empty;
            try
            {
                //nhibernate transaction IsolationLevel is null if Outbox is enabled
                nhIsolationLevel = ((NHibernate.Transaction.AdoTransaction)((NHibernate.Impl.SessionImpl)session).Transaction).IsolationLevel.ToString();
            }
            catch (Exception ex)
            {
                nhIsolationLevel = ex.Message;
            }
            log.Warn($"NH IsolationLevel: {nhIsolationLevel}");

            var orderShipped = new OrderShipped
            {
                OrderId = message.OrderId,
                ShippingDate = message.ShippingDate,
            };

            session.Save(orderShipped); //We want to use EntityFramework here instead of NHibernate, but I left it out to simplify.

            bus.Publish<OrderShippedEvent>(e =>
            {
                e.Id = message.OrderId;
                e.ShippingDate = message.ShippingDate;
            });
        }
    }
}