using DocumentServicesSaga.Messages.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace DocumentServices.Saga.Web.Handlers
{
    public class DocumentExtractedHandler : IHandleMessages<DocumentExtracted> 
    {
        public void Handle(DocumentExtracted message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<DocumentsHub>();
            hubContext.Clients.Client(message.ClientId).documentExtracted(new
            {
                message.FilePath,
                message.DocumentId
            });
        }
    }
}