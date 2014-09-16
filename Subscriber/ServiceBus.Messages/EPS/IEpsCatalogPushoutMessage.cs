using System;
using NServiceBus;

namespace Hinda.Internal.ServiceBus.Messages.EPS
{
    public interface IEpsPushoutClientCatalogMessage : IMessage
    {
        string DatabaseServer { get; set; }
        string DatabaseName { get; set; }
        int PriceListId { get; set; }
        DateTime Date { get; set; }
        bool UpdateFromPeopleSoft { get; set; }
    }
}