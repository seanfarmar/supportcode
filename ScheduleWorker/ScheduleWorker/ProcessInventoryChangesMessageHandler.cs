namespace ScheduleWorker
{
    using System;
    using Dependencies;
    using MyCorp.NSB.Contracts.Commands;
    using MyCorp.NSB.Contracts.Events;
    using NServiceBus;

    public class ProcessInventoryChangesMessageHandler : IHandleMessages<ProcessInventoryChanges>
    {
        private readonly IInventoryDataProvider _inventoryDataProvider;

        public ProcessInventoryChangesMessageHandler(IInventoryDataProvider inventoryDataProvider)
        {
            _inventoryDataProvider = inventoryDataProvider;
        }

        public IBus Bus { get; set; }

        public void Handle(ProcessInventoryChanges message)
        {
            // Do the work here, one for each unit of work, thei handler can scale out to it's own endpoint
            var inventory = _inventoryDataProvider.GetInventory();

            Console.WriteLine("Handeling ProcessInventoryChanges and inventoryCode is: {0}, Invetory text form InventoryDataProvider: {1}", message.InventoryCode, inventory);

            // now we publish....
            Bus.Publish(new InventoryChanged
            {
                Guid = message.Guid,
                Inventory = inventory,
                InventoryCode = message.InventoryCode
            });

            Console.WriteLine("Published InventoryChanged event inventoryCode is: {0}", message.InventoryCode);
        }
    }
}