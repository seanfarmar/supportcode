using Messages;
using NServiceBus;

namespace Server.WebServices
{
    public class CancelOrderTwoService : WcfService<CancelOrderTwo, ErrorCodes>
    {
    }
}