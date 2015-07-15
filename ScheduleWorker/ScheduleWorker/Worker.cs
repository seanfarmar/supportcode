using System;
using MyCorp.NSB.Contracts.Commands;
using NServiceBus;

namespace ScheduleWorker
{
    public class Worker
    {
        private readonly IBus bus;

        public Worker(IBus bus)
        {
            this.bus = bus;
        }

        public void DoWork()
        {
            Console.WriteLine("Sending ProcessInventoryChangesMessage");
            // using SendLocal for simlicity  
            bus.SendLocal(new ProcessInventoryChangesMessage { Guid = Guid.NewGuid(), InventoryCode = "INV_001" });

            // same for the rest:
            // Bus.Send(new ProcessSalesOrderChanges());
            // Bus.Send(new ProcessSalesShipmentChanges());
        }
    }
}