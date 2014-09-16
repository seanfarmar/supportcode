using System.Collections.Generic;
using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    public interface IEpsImportItemsToGlobalShareMessage : IMessage
    {
        List<int> ItemIds { get; set; }
    }
}
