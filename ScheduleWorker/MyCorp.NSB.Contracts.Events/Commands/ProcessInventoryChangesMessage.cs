namespace MyCorp.NSB.Contracts.Commands
{
    using System;

    public class ProcessInventoryChangesMessage
    {
        public Guid Guid { get; set; }

        public string InventoryCode { get; set; }
    }
}
