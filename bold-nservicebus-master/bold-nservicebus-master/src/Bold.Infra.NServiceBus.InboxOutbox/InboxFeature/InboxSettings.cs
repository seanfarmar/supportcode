namespace Bold.Infra.NServiceBus.InboxOutbox.InboxFeature
{
    public class InboxSettings : IInboxSettings
    {
        public InboxSettings(string schemaName)
        {
            SchemaName = schemaName;
        }

        public string SchemaName { get; set; }
    }
}