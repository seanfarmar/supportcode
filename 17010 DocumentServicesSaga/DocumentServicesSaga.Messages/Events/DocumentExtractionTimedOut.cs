using System;

namespace DocumentServicesSaga.Messages.Events
{
    public class DocumentExtractionTimedOut
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
