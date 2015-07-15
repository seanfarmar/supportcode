namespace ScheduleWorker
{
    using System;
    using Common;
    using MessageConventions;
    using Ninject;
    using NServiceBus;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Common code to define all the interface bindings
            using (var kernel = NinjectCommon.Start())
            {
                var busConfiguration = CreateBusConfig(kernel); // Bus is registered in Ninject container by NServiceBus

                using (Bus.Create(busConfiguration).Start())
                {
                    var worker = kernel.Get<Worker>();

                    worker.DoWork();

                    // only for demo
                    Console.WriteLine("Hit any key to exit");
                    Console.ReadKey();
                } // This makes sure that the bus is properly disposed thus shutdown.
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
}