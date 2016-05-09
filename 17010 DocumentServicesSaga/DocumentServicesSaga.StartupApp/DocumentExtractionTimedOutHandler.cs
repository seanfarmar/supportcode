using System;
using DocumentServicesSaga.Messages.Events;
using NServiceBus;

namespace DocumentServicesSaga.StartupApp
{
    public class DocumentExtractionTimedOutHandler : IHandleMessages<DocumentExtractionTimedOut>
    {
        IBus bus;

        public DocumentExtractionTimedOutHandler(IBus bus)
        {
            this.bus = bus;
        }
        public void Handle(DocumentExtractionTimedOut message)
        {
            Console.WriteLine("Document {0} extraction failed due to timeout", message.FilePath);
        }
    }
}
