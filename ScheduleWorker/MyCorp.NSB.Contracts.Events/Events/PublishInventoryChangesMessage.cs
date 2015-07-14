namespace MyCorp.NSB.Contracts.Events
{
    using System;

    public class PublishInventoryChangesMessage
    {
        public Guid Guid { get; set; }

        public string Inventory { get; set; }

        public string InventoryCode { get; set; }
    }
}
