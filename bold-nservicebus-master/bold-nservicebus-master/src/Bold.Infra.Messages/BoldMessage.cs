using System;

namespace Bold.Infra.Messages
{
    public class BoldMessage : IBoldMessage
    {
        public BoldMessage(Guid contentId)
            : this(contentId.ToString())
        {
        }

        public BoldMessage(string contentId)
            : this(contentId, DateTime.UtcNow.Ticks)
        {
        }

        public BoldMessage(int contentId)
            : this(contentId.ToString(), DateTime.UtcNow.Ticks)
        {
        }

        public BoldMessage(Guid contentId, long contentVersion)
            : this(contentId.ToString(), contentVersion)
        {
        }

        public BoldMessage(int contentId, long contentVersion)
            : this(contentId.ToString(), contentVersion)
        {
        }

        public BoldMessage(string contentId, long contentVersion)
        {
            ContentId = contentId;
            ContentVersion = contentVersion;
            CreatedAt = DateTime.UtcNow;
        }

        public string ContentId { get; set; }
        public long ContentVersion { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}