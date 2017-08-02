namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public interface IInboxRepository
    {
        void PersistHandledMessage(InboxMessage inboxMessage);
        void PersistDiscardedMessage(InboxMessage inboxMessage, long latestHandledVersion);
        long? GetLatestMessageVersion(string contentId, string checkMessageOrderType);
    }
}