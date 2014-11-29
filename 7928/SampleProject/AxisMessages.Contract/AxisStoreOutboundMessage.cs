using System;

namespace DataSync.Common.AxisMessages.Contract
{
    public abstract class AxisStoreOutboundMessage : AxisStoreMessage
    {
        public string Identifier { get; set; }
    }
}