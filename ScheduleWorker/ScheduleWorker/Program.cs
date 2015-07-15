namespace ScheduleWorker
{
    using System;
    using MessageConventions;
    using Ninject;
    using NServiceBus;
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Common code to define all the interface bindings
            using (var kernel = NinjectCommon.Start())
            {
                var busConfiguration = CreateBusConfig(kernel);  // Bus is registered in Ninject container by NServiceBus

                using (Bus.Create(busConfiguration).Start())
                {
                    var worker = kernel.Get<Worker>();

                    worker.DoWork();

                    // only for demo
                    Console.WriteLine("Hit any key to exit");
                    Console.ReadKey();

                }// This makes sure that the bus is properly disposed thus shutdown.
            }
        }

        private static BusConfiguration CreateBusConfig(IKernel kernel)
        {
            var configuration = new BusConfiguration();
            configuration.UseContainer<NinjectBuilder>(b => b.ExistingKernel(kernel));
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.ApplyMessageConventions();
            //configuration.EnableInstallers();
            return configuration;
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