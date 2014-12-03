namespace DataSync.Common.AxisMessages.Contract
{
    public abstract class AxisHQOutboundMessage : AxisMessage
    {
        public string Identifier { get; set; }
    }
}