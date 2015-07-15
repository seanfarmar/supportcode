namespace ScheduleWorker
{
    using System;
    using MessageConventions;
    using MyCorp.NSB.Contracts.Commands;
    using Ninject;
    using NServiceBus;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new BusConfiguration();

            // Common code to define all the interface bindings
            var kernel = NinjectCommon.Start();

            configuration.UseContainer<NinjectBuilder>(b => b.ExistingKernel(kernel));

            new NServiceBusConventions().Customize(configuration);

            configuration.ApplyMessageConventions();
            
            var bus = Bus.Create(configuration).Start();

            new Worker().DoWork(bus);

            // only for demo
            Console.WriteLine("Hit any key to exit");
            Console.ReadKey();
        }
    }

    public class Worker
    {
        public void DoWork(IBus bus)
        {
            // using SendLocal for simlicity  
            bus.SendLocal(new ProcessInventoryChangesMessage{Guid = Guid.NewGuid(), InventoryCode = "INV_001"});

            // same for the rest:
            // Bus.Send(new ProcessSalesOrderChanges());
            // Bus.Send(new ProcessSalesShipmentChanges());
        }
    }

    public class NServiceBusConventions : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
        }
    }

    public class ErrorQueueProvider : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig { ErrorQueue = "error" };
        }
    }

    public class AuditQueueProvider : IProvideConfiguration<AuditConfig>
    {
        public AuditConfig GetConfiguration()
        {
            return new AuditConfig { QueueName = "audit" };
        }
    }

    public class NinjectCommon
    {
        public static IKernel Start()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IInventoryDataProvider>().ToConstant(new InventoryDataProvider());

            return kernel;
        }
    }
}