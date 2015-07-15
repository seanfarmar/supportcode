namespace MyCorp.NSB.Contracts.Commands
{
    using System;

    public class ProcessInventoryChanges
    {
        public Guid Guid { get; set; }
        public string InventoryCode { get; set; }
    }
}