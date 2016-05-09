namespace DocumentServicesSaga.Messages.Commands
{
    using System;

    public class ExtractDocument
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
