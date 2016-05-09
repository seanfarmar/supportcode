using System;
using DocumentServicesSaga.Messages.Commands;
using Microsoft.AspNet.SignalR;

namespace DocumentServices.Saga.Web.Handlers
{
    public class DocumentsHub : Hub
    {
        public void ExtractDocument(string filePath)
        {

            var command = new DocumentExtractionRequest
            {
                DocumentId = Guid.NewGuid(),
                ClientId = Context.ConnectionId,
                FilePath = filePath
            };

            MvcApplication.Bus.Send(command);
        }
    }
}