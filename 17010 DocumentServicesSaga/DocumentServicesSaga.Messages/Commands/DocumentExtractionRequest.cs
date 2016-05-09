using System;

namespace DocumentServicesSaga.Messages.Commands
{
    public class DocumentExtractionRequest
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
