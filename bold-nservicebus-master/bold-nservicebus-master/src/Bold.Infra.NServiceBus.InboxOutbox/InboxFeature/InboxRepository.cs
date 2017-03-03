using System.Data.SqlClient;
using System.Linq;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class InboxRepository : IInboxRepository
    {
        private readonly InboxDbContext dbContext;

        public InboxRepository(InboxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void PersistHandledMessage(InboxMessage inboxMessage)
        {
            //Add or Update without locking table/page
            const string sql = @"MERGE INTO [{0}].InboxRecord WITH (updlock, rowlock) AS tgt
                        USING
                          (SELECT @ContentId, @CheckMessageOrderType) AS src (ContentId, CheckMessageOrderType)
                          ON tgt.ContentId = src.ContentId and tgt.CheckMessageOrderType = src.CheckMessageOrderType
                        WHEN MATCHED THEN
                          UPDATE       
                            SET 
                                ContentVersion = @ContentVersion, 
                                ContentId = @ContentId, 
                                ModifiedAtUtc = @ModifiedAtUtc,
                                MessageId = @MessageId 
                        WHEN NOT MATCHED THEN
                          INSERT (ContentId, ContentVersion, CheckMessageOrderType, ModifiedAtUtc, MessageId) 
                          VALUES (@ContentId, @ContentVersion, @CheckMessageOrderType, @ModifiedAtUtc, @MessageId);";

            var schemaName = dbContext.Settings.SchemaName;
            dbContext.Database.ExecuteSqlCommand(
                string.Format(sql, schemaName),
                new SqlParameter("@ContentId", inboxMessage.ContentId),
                new SqlParameter("@ContentVersion", inboxMessage.ContentVersion),
                new SqlParameter("@CheckMessageOrderType", inboxMessage.CheckMessageOrderType),
                new SqlParameter("@ModifiedAtUtc", inboxMessage.ModifiedAtUtc),
                new SqlParameter("@MessageId", inboxMessage.MessageId));
        }

        public void PersistDiscardedMessage(InboxMessage inboxMessage, long latestHandledVersion)
        {
            //Add or update
            const string sql = @"INSERT INTO [{0}].[InboxRecordDiscarded] (ContentId, ContentVersion, LatestContentVersion, CheckMessageOrderType, ModifiedAtUtc, MessageId)
		                        VALUES (@ContentId, @ContentVersion, @LatestContentVersion, @CheckMessageOrderType, @ModifiedAtUtc, @MessageId)";

            var schemaName = dbContext.Settings.SchemaName;
            dbContext.Database.ExecuteSqlCommand(
                string.Format(sql, schemaName),
                new SqlParameter("@ContentId", inboxMessage.ContentId),
                new SqlParameter("@ContentVersion", inboxMessage.ContentVersion),
                new SqlParameter("@LatestContentVersion", latestHandledVersion),
                new SqlParameter("@CheckMessageOrderType", inboxMessage.CheckMessageOrderType),
                new SqlParameter("@ModifiedAtUtc", inboxMessage.ModifiedAtUtc),
                new SqlParameter("@MessageId", inboxMessage.MessageId));
        }

        public long? GetLatestMessageVersion(string contentId, string checkMessageOrderType)
        {
            return dbContext.InboxRecords
                .Where(x => x.ContentId == contentId && x.CheckMessageOrderType == checkMessageOrderType)
                .Select(x => x.ContentVersion)
                .SingleOrDefault();
        }
    }
}