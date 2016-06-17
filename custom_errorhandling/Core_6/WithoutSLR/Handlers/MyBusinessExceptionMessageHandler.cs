using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BaseException;
using NServiceBus;
using NServiceBus.Logging;

#region Handler
public class MyBusinessExceptionMessageHandler : IHandleMessages<MyBusinessExceptionMessage>
{
    static ILog log = LogManager.GetLogger<MyOKMessageHandler>();
    static ConcurrentDictionary<Guid, string> Last = new ConcurrentDictionary<Guid, string>();

    public Task Handle(MyBusinessExceptionMessage okMessage, IMessageHandlerContext context)
    {
        log.Info($"ReplyToAddress: {context.ReplyToAddress} MessageId:{context.MessageId}");

        // add a MyBusinessException exception to test behavior     
        throw new MyBusinessException("A MyBaseException occurred in the handler.");
    }
}
#endregion