namespace DataSync.Common.AxisMessages.Contract
{
    using System;
    
    public abstract class AxisMessage
    {
        public int CompanyID { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
    }
}