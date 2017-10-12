using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;
using Store.Messages.Commands;
using Store.Messages.Events;

public class ProcessOrderSaga :
    SqlSaga<ProcessOrderSaga.OrderData>,
    IAmStartedByMessages<SubmitOrder>,
    IHandleMessages<CancelOrder>,
    IHandleTimeouts<ProcessOrderSaga.BuyersRemorseIsOver>
{
    static ILog log = LogManager.GetLogger<ProcessOrderSaga>();

    public Task Handle(SubmitOrder message, IMessageHandlerContext context)
    {
        if (DebugFlagMutator.Debug)
        {
            Debugger.Break();
        }

        Data.OrderNumber = message.OrderNumber;
        Data.ProductIds = message.ProductIds;
        Data.ClientId = message.ClientId;

        log.Info($"Starting cool down period for order #{Data.OrderNumber}.");
        return RequestTimeout(context, TimeSpan.FromSeconds(20), new BuyersRemorseIsOver());
    }

    public Task Timeout(BuyersRemorseIsOver state, IMessageHandlerContext context)
    {
        if (DebugFlagMutator.Debug)
        {
            Debugger.Break();
        }

        log.Info($"Cooling down period for order #{Data.OrderNumber} has elapsed.");

        var orderAccepted = new OrderAccepted
        {
            OrderNumber = Data.OrderNumber,
            ProductIds = Data.ProductIds,
            ClientId = Data.ClientId
        };

        MarkAsComplete();
        return context.Publish(orderAccepted);
    }

    public Task Handle(CancelOrder message, IMessageHandlerContext context)
    {
        if (DebugFlagMutator.Debug)
        {
            Debugger.Break();
        }

        MarkAsComplete();

        log.Info($"Order #{message.OrderNumber} was cancelled.");

        var orderCancelled = new OrderCancelled
        {
            OrderNumber = message.OrderNumber,
            ClientId = message.ClientId
        };
        return context.Publish(orderCancelled);
    }

    public class OrderData :
        ContainSagaData
    {
        public int OrderNumber { get; set; }
        public string[] ProductIds { get; set; }
        public string ClientId { get; set; }
    }

    public class BuyersRemorseIsOver
    {
    }

    protected override void ConfigureMapping(IMessagePropertyMapper mapper)
    {
        mapper.ConfigureMapping<SubmitOrder>(_ => _.OrderNumber);

        mapper.ConfigureMapping<CancelOrder>(_ => _.OrderNumber);
    }

    protected override string CorrelationPropertyName => nameof(OrderData.OrderNumber);
}