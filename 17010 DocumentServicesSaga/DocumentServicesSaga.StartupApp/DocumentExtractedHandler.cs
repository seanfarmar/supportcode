using System;
using DocumentServicesSaga.Messages.Events;
using NServiceBus;

namespace DocumentServicesSaga.StartupApp
{
    public class DocumentExtractedHandler : IHandleMessages<DocumentExtracted>
    {
        IBus bus;

        public DocumentExtractedHandler(IBus bus)
        {
            this.bus = bus;
        }
        public void Handle(DocumentExtracted message)
        {
            Console.WriteLine("Document has been extracted: {0} at {1}", message.FilePath, DateTime.Now);
        }
    }
}
