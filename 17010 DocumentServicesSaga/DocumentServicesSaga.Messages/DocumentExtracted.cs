using System;

namespace DocumentServicesSaga.Messages
{
    public class DocumentExtractedReply
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
