using System;
using System.Linq;
using Bold.Infra.Messages;
using NServiceBus.Logging;
using NServiceBus.Pipeline;
using NServiceBus.Pipeline.Contexts;
using NServiceBus.Unicast.Messages;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class InboxBehavior : IBehavior<IncomingContext>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InboxBehavior).Name);
        private readonly IInboxRepository inboxRepository;

        public InboxBehavior(IInboxRepository inboxRepository)
        {
            if (inboxRepository == null)
            {
                throw new ArgumentNullException(nameof(inboxRepository));
            }
            this.inboxRepository = inboxRepository;
        }

        public void Invoke(IncomingContext context, Action next)
        {
            var logicalMessage = context.IncomingLogicalMessage;

            var contentId = logicalMessage.Headers[BoldMessageHeaders.ContentId];
            var contentVersion = long.Parse(logicalMessage.Headers[BoldMessageHeaders.ContentVersion]);

            var checkMessageOrderType = GetCheckMessageOrderType(logicalMessage);

            var latestHandledVersion = inboxRepository.GetLatestMessageVersion(contentId, checkMessageOrderType);

            var shouldBeHandled = latestHandledVersion == null || contentVersion > latestHandledVersion;

            var inboxMessage = new InboxMessage
            {
                ContentId = contentId,
                ContentVersion = contentVersion,
                CheckMessageOrderType = checkMessageOrderType,
                MessageId = Guid.Parse(context.PhysicalMessage.Id),
                ModifiedAtUtc = DateTime.UtcNow
            };

            if (shouldBeHandled)
            {
                PersistHandledMessage(inboxMessage);
                next();
            }
            else
            {
                PersistDiscardedMessage(inboxMessage, latestHandledVersion.GetValueOrDefault());
            }
        }

        private void PersistHandledMessage(InboxMessage inboxMessage)
        {
            inboxRepository.PersistHandledMessage(inboxMessage);
        }

        private void PersistDiscardedMessage(InboxMessage inboxMessage, long latestHandledVersion)
        {
            inboxRepository.PersistDiscardedMessage(inboxMessage, latestHandledVersion);
        }

        private static string GetCheckMessageOrderType(LogicalMessage message)
        {
            if (message.Headers.ContainsKey(BoldMessageHeaders.CheckMessageOrderType))
            {
                return message.Headers[BoldMessageHeaders.CheckMessageOrderType];
            }

            var checkMessageOrder = ChecksMessageOrderBy(message.MessageType, typeof(ICheckMessageOrder<>));
            return checkMessageOrder == null ? message.MessageType.FullName : checkMessageOrder.GetGenericArguments()[0].FullName;
        }

        private static Type ChecksMessageOrderBy(Type typeToExamine, Type genericInterfaceToLookFor)
        {
            var @interface = typeToExamine.GetInterfaces().SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceToLookFor);
            return @interface;
        }

        public class InboxRegistration : RegisterStep
        {
            public InboxRegistration()
                : base("InboxBehavior", typeof(InboxBehavior), "Inbox behavior")
            {
                InsertAfterIfExists("OutboxDeduplication");
                InsertBefore(WellKnownStep.LoadHandlers);
            }
        }
    }
}
