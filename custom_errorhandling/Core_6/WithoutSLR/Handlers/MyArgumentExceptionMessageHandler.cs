using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

#region Handler
public class MyArgumentExceptionMessageHandler : IHandleMessages<MyArgumentExceptionMessage>
{
    static ILog log = LogManager.GetLogger<MyOKMessageHandler>();
    static ConcurrentDictionary<Guid, string> Last = new ConcurrentDictionary<Guid, string>();

    public Task Handle(MyArgumentExceptionMessage okMessage, IMessageHandlerContext context)
    {
        log.Info($"ReplyToAddress: {context.ReplyToAddress} MessageId:{context.MessageId}");
        
        // add a regular exception modulator to test normal behavior     
        throw new ArgumentException("A MyArgumentException occurred in the handler.");
    }
}
#endregion