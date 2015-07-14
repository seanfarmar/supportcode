namespace ScheduleWorker
{
    using System;
    using MyCorp.NSB.Contracts.Commands;
    using MyCorp.NSB.Contracts.Events;
    using NServiceBus;

    public class ProcessInventoryChangesMessageHandler : IHandleMessages<ProcessInventoryChangesMessage>
    {
        readonly IInventoryDataProvider _inventoryDataProvider;
        public IBus Bus { get; set; }

        public ProcessInventoryChangesMessageHandler(IInventoryDataProvider inventoryDataProvider)
        {
            _inventoryDataProvider = inventoryDataProvider;
        }

        public void Handle(ProcessInventoryChangesMessage message)
        {
            // Do the work here, one for each unit of work, thei handler can scale out to it's own endpoint

            string inventory = _inventoryDataProvider.GetInventory();

            Console.WriteLine("Handeling ProcessInventoryChangesMessage and inventory is: {0}", inventory);

            // now we publish....
            Bus.Publish(new PublishInventoryChangesMessage{Guid=message.Guid, Inventory = inventory, InventoryCode= message.InventoryCode});
        }
    }
}
