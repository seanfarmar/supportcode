using DocumentServicesSaga.Messages.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace DocumentServices.Saga.Web.Handlers
{
    public class DocumentExtractionTimedOutHandler : IHandleMessages<DocumentExtractionTimedOut>
    {
        public void Handle(DocumentExtractionTimedOut message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<DocumentsHub>();
            hubContext.Clients.Client(message.ClientId).documentExtractionTimedOut(new
            {
                message.FilePath,
                message.DocumentId
            });
        }
    }
}