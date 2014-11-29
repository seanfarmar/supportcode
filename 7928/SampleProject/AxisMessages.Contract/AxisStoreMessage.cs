using System;

namespace DataSync.Common.AxisMessages.Contract
{
    public abstract class AxisStoreMessage : AxisMessage
    {
        public int CompanyStoreID { get; set; }
    }
}