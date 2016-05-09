using System;
using System.Threading;
using System.Threading.Tasks;
using DocumentServicesSaga.Messages.Commands;
using DocumentServicesSaga.Messages.Events;
using DocumentServicesSaga.Shared;
using NServiceBus;
using NServiceBus.Saga;

namespace DocumentServices.Saga
{
    class DocumentServicesSaga : Saga<DocumentData>,
                                IAmStartedByMessages<DocumentExtractionRequest>,
                                IHandleMessages<DocumentExtracted>,
                                IHandleTimeouts<DocumentExtractionTimeout>
    {
        
        public void Handle(DocumentExtractionRequest message)
        {
            Data.DocumentId = message.DocumentId;
            Data.FilePath = message.FilePath;
            Data.ClientId = message.ClientId;

            Console.WriteLine("Got document extraction request at {0}", DateTime.Now);
            
            var task = new Task(ExtractDocument);

            task.Start();

            RequestTimeout<DocumentExtractionTimeout>(TimeSpan.FromSeconds(15));
        }

        public void Handle(DocumentExtracted message)
        {
            Console.WriteLine("Extraction completed");
            MarkAsComplete();
        }


        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<DocumentData> mapper)
        {
            mapper.ConfigureMapping<DocumentExtractionRequest>(message => message.DocumentId).ToSaga(sagaData => sagaData.DocumentId);
            mapper.ConfigureMapping<DocumentExtracted>(message => message.DocumentId).ToSaga(sagaData => sagaData.DocumentId);
        }


        private void ExtractDocument()
        {
            var ts = new TimeSpan(0, 0, 0, 25);
            Console.WriteLine("Waiting {0}", ts.ToFriendlyDisplay(3));
            Thread.Sleep(ts);

            var documentExtracted = new DocumentExtracted
            {
                ClientId = Data.ClientId,
                DocumentId = Data.DocumentId,
                FilePath = Data.FilePath
            };

            Bus.Publish(documentExtracted);

        }


        public void Timeout(DocumentExtractionTimeout state)
        {
            MarkAsComplete();

            Console.WriteLine("Document extraction failed to complete prior to timeout.");
            var documentTimedOut = new DocumentExtractionTimedOut
            {
                ClientId = Data.ClientId,
                DocumentId = Data.DocumentId,
                FilePath = Data.FilePath
            };
            
            Bus.Publish(documentTimedOut);


        }

    }


    public class DocumentData : ContainSagaData
    {
        [Unique]
        public virtual Guid DocumentId { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string ClientId { get; set; }
    }

    public class DocumentExtractionTimeout
    {
        public Guid DocumentId { get; set; }
        public string FilePath { get; set; }
        public string ClientId { get; set; }
    }
}
