using System;

namespace DocumentServices.Saga
{
    public class DocumentExtractionTimeout
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
