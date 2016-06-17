using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

#region Handler
public class MyOKMessageHandler : IHandleMessages<MyOkMessage>
{
    static ILog log = LogManager.GetLogger<MyOKMessageHandler>();
    static ConcurrentDictionary<Guid, string> Last = new ConcurrentDictionary<Guid, string>();

    public Task Handle(MyOkMessage okMessage, IMessageHandlerContext context)
    {
        log.Info($"ReplyToAddress: {context.ReplyToAddress} MessageId:{context.MessageId}");
       
        return Task.FromResult(0);
    }
}
#endregion