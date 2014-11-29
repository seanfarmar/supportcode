using System;
using NServiceBus;

namespace DataSync.Common.AxisMessages.Contract
{
    public abstract class AxisMessage : IMessage
    {
        public int CompanyID { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
    }
}