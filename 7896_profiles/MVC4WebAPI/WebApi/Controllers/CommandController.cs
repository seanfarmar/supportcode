namespace WebApi.Controllers
{
    using System.Web.Http;
    using Messages;
    using Models;

    public class CommandController : ApiController
    {
        // POST api/command
        public void PostProduct(Command item)
        {
            var commandMessage = new CommandMessage {Id = item.Id, Name = item.Name};
            
            NServiceBusBootstrapper.Bus.Send(commandMessage);
        }
    }
}
