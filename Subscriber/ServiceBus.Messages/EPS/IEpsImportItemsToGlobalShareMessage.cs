namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    using System.Collections.Generic;
    using NServiceBus;

    public interface IEpsImportItemsToGlobalShareMessage : IMessage
    {
        List<int> ItemIds { get; set; }
    }
}