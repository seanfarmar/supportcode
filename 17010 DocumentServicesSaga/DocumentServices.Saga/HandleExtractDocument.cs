namespace DocumentServices.Saga
{
    using System;
    using System.Threading;
    using DocumentServicesSaga.Messages;
    using DocumentServicesSaga.Messages.Commands;
    using DocumentServicesSaga.Shared;
    using NServiceBus;

    class HandleExtractDocument : IHandleMessages<ExtractDocument>
    {
        public IBus Bus { get; set; }

        public void Handle(ExtractDocument message)
        {
            var ts = new TimeSpan(0, 0, 0, 25);
            Console.WriteLine("Waiting {0}", ts.ToFriendlyDisplay(3));
            Thread.Sleep(ts);

            var documentExtracted = new DocumentExtractedReply
            {
                ClientId = message.ClientId,
                DocumentId = message.DocumentId,
                FilePath = message.FilePath
            };

            Bus.Reply(documentExtracted);
        }
    }
}
