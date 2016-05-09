namespace DocumentServices.Saga
{
    using System;
    using DocumentServicesSaga.Messages;
    using DocumentServicesSaga.Messages.Commands;
    using DocumentServicesSaga.Messages.Events;
    using NServiceBus;
    using NServiceBus.Saga;

    class DocumentServicesProcessSaga : Saga<DocumentSagaData>,
        IAmStartedByMessages<DocumentExtractionRequest>,
        IHandleTimeouts<DocumentExtractionTimeout>,
        IHandleMessages<DocumentExtractedReply>
    {
        public void Handle(DocumentExtractionRequest message)
        {
            Data.DocumentId = message.DocumentId;
            Data.FilePath = message.FilePath;
            Data.ClientId = message.ClientId;

            Console.WriteLine("Got document extraction request at {0}", DateTime.Now);
            
            var extractDocument = new ExtractDocument
            {
                DocumentId = message.DocumentId,
                ClientId = message.ClientId,
                FilePath = message.FilePath
            };

            // using sendlocal for simplicity 
            Bus.SendLocal(extractDocument);

            RequestTimeout<DocumentExtractionTimeout>(TimeSpan.FromSeconds(15));
        }
        
        public void Handle(DocumentExtractedReply message)
        {
            Console.WriteLine("Extraction completed from reply");

            Bus.Publish(new DocumentExtracted { ClientId = message.ClientId, FilePath = message.FilePath, DocumentId = message.DocumentId });

            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<DocumentSagaData> mapper)
        {
            mapper.ConfigureMapping<DocumentExtractionRequest>(message => message.DocumentId).ToSaga(sagaData => sagaData.DocumentId);
            mapper.ConfigureMapping<DocumentExtracted>(message => message.DocumentId).ToSaga(sagaData => sagaData.DocumentId);
        }

        public void Timeout(DocumentExtractionTimeout state)
        {
            Console.WriteLine("Document extraction failed to complete prior to timeout.");

            var documentTimedOut = new DocumentExtractionTimedOut
            {
                ClientId = Data.ClientId,
                DocumentId = Data.DocumentId,
                FilePath = Data.FilePath
            };
            
            Bus.Publish(documentTimedOut);

            MarkAsComplete();
        }
    }
}