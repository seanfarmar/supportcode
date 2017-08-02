using System;
using System.Data.Entity;
using System.Linq;

namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class InboxStartupDbContext : InboxDbContext
    {
        private const string createSchemaSql = @"IF NOT EXISTS (
                                                    SELECT  schema_name
                                                    FROM    information_schema.schemata
                                                    WHERE   schema_name = '{0}') 

                                                    BEGIN
                                                    EXEC sp_executesql N'CREATE SCHEMA {0}'
                                                    END";

        private const string createInboxRecordSql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[InboxRecord]') AND type in (N'U'))
                                                    BEGIN
                                                     CREATE TABLE [{0}].[InboxRecord](
	                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                    [ContentId] [nvarchar](100) NULL,
	                                                    [ContentVersion] [bigint] NOT NULL,
	                                                    [CheckMessageOrderType] [nvarchar](300) NULL,
	                                                    [MessageId] [uniqueidentifier] NOT NULL,
	                                                    [ModifiedAtUtc] [datetime] NOT NULL,
                                                        CONSTRAINT [PK_{0}.InboxRecord] PRIMARY KEY CLUSTERED 
                                                    (
	                                                    [Id] ASC
                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                    ) ON [PRIMARY]
                                                    END";

        private const string createInboxRecordIndexSql = @"IF NOT EXISTS (SELECT * FROM sys.indexes 
			                                                              WHERE name='UIX_InboxRecord_ContentId_CheckMessageOrderType' AND object_id = OBJECT_ID('[{0}].[InboxRecord]'))
                                                         BEGIN
															CREATE UNIQUE NONCLUSTERED INDEX UIX_InboxRecord_ContentId_CheckMessageOrderType
															ON [{0}].[InboxRecord] (ContentId, CheckMessageOrderType)
                                                         END";

        private const string createInboxRecordIndexModifiedAtUtcSql = @"IF NOT EXISTS (SELECT * FROM sys.indexes 
			                                                              WHERE name='nci_wi_InboxRecord_7D5FF081EC49CEF4F8D494FA2CFBE5B2' AND object_id = OBJECT_ID('[{0}].[InboxRecord]'))
                                                         BEGIN
															CREATE NONCLUSTERED INDEX [nci_wi_InboxRecord_7D5FF081EC49CEF4F8D494FA2CFBE5B2] ON [{0}].[InboxRecord] ([ModifiedAtUtc]) INCLUDE ([CheckMessageOrderType], [ContentId], [ContentVersion], [MessageId]) WITH (ONLINE = ON)
                                                         END";

        private const string createInboxDiscardedRecordSql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[InboxRecordDiscarded]') AND type in (N'U'))
                                                    BEGIN
                                                    CREATE TABLE [{0}].[InboxRecordDiscarded](
	                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                                                    [ContentId] [nvarchar](100) NULL,
	                                                    [ContentVersion] [bigint] NOT NULL,
	                                                    [LatestContentVersion] [bigint] NOT NULL,
	                                                    [CheckMessageOrderType] [nvarchar](300) NULL,
	                                                    [MessageId] [uniqueidentifier] NOT NULL,
	                                                    [ModifiedAtUtc] [datetime] NOT NULL,
                                                        CONSTRAINT [PK_{0}.InboxRecordDiscarded] PRIMARY KEY CLUSTERED 
                                                    (
	                                                    [Id] ASC
                                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                    ) ON [PRIMARY]
                                                    END";

        private const string createInboxRecordModifiedAtUtcIndexSql = @"IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name='Index-ModifiedAtUtc-Descending' AND object_id = OBJECT_ID('[{0}].[InboxRecord]'))
                                                                        BEGIN
	                                                                        CREATE NONCLUSTERED INDEX [Index-ModifiedAtUtc-Descending] ON [{0}].[InboxRecord]
	                                                                        (
		                                                                        [ModifiedAtUtc] DESC
	                                                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)	
                                                                        END;";

        public InboxStartupDbContext()
        {
            TurnOffAutomaticDatabaseCreationAndSchemaUpdates();
        }

        public InboxStartupDbContext(string connectionString, IInboxSettings settings) : base(connectionString, settings)
        {
            TurnOffAutomaticDatabaseCreationAndSchemaUpdates();
        }

        public void RemoveEntriesOlderThan(DateTime dateTime)
        {
            var entriesToRemove = InboxRecords.Where(e => e.ModifiedAtUtc < dateTime).ToList();
            if (!entriesToRemove.Any())
            {
                return;
            }

            InboxRecords.RemoveRange(entriesToRemove);
            SaveChanges();
        }

        public void InitializeDatabase()
        {
            var schemaName = Settings.SchemaName;
            Database.ExecuteSqlCommand(string.Format(createSchemaSql, schemaName));
            Database.ExecuteSqlCommand(string.Format(createInboxRecordSql, schemaName));
            Database.ExecuteSqlCommand(string.Format(createInboxRecordIndexSql, schemaName));
            Database.ExecuteSqlCommand(string.Format(createInboxRecordIndexModifiedAtUtcSql, schemaName));
            Database.ExecuteSqlCommand(string.Format(createInboxDiscardedRecordSql, schemaName));
            Database.ExecuteSqlCommand(string.Format(createInboxRecordModifiedAtUtcIndexSql, schemaName));
        }

        private static void TurnOffAutomaticDatabaseCreationAndSchemaUpdates()
        {
            Database.SetInitializer<InboxStartupDbContext>(null);
        }
    }
}