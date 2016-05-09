using System;

namespace DocumentServicesSaga.Messages.Events
{
    public class DocumentExtracted
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
