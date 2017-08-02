using System;
using System.Threading.Tasks;
using Env = NHibernate.Cfg.Environment;
using NServiceBus.Persistence;
using NServiceBus;

namespace Test
{
    using Fbs.Contracts.Messages.Commands;

    class Program
    {
        static void Main()
        {
            AsyncOnStart().GetAwaiter().GetResult();
        }
        
        static async Task AsyncOnStart()
        {
            //TODO:Replace 
            string configEndpointName = "Test.Publisher.ClientPayments";

            Console.Title = "Test.Publisher.ClientPayments";

            if (string.IsNullOrEmpty(configEndpointName))
                    throw new ArgumentNullException(configEndpointName, "Endpoint name cannot be null");

            // build config
            var endpointConfiguration = new EndpointConfiguration(configEndpointName);
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            endpointConfiguration.UseSerialization<JsonSerializer>();

            //enable installers for DEV purposes
            endpointConfiguration.EnableInstallers();// TODO: Move to the appropriate place

            //setup Persistence
            var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();

            //var assemblyScanner = endpointConfiguration.AssemblyScanner();
            //assemblyScanner.ExcludeAssemblies("Fbs.Hosts.Endpoints.TestClientPayments.vshost.exe", "Fbs.Hosts.Endpoints.ClientPayments.exe","Fbs.Infrastructure.Messaging.dll", "Fbs.Endpoints.ClientPayments.dll", "Fbs.Common.dll", "Fbs.Handlers.dll");
                
            var nhConfig = new NHibernate.Cfg.Configuration();
            nhConfig.SetProperty(Env.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            nhConfig.SetProperty(Env.ConnectionDriver, "NHibernate.Driver.Sql2008ClientDriver");
            nhConfig.SetProperty(Env.Dialect, "NHibernate.Dialect.MsSql2008Dialect");
            nhConfig.SetProperty(Env.ConnectionStringName, "Test.Publisher.ClientPayments.Persistence");
            persistence.UseConfiguration(nhConfig);

            //setup transport
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
            .ConnectionStringName("Test.Publisher.ClientPayments.Transport");

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PaymentReceived), "Fbs.Hosts.Endpoints.ClientPayments");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await SendOrder(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }
        
        static async Task SendOrder(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                var id = "Zoro" + DateTime.Now.Millisecond; //Guid.NewGuid();

                var placeOrder = new PaymentReceived {Id = id};
               
                await endpointInstance.Send(placeOrder);//Fbs.Hosts.Endpoints.ClientPayments
                
                Console.WriteLine(String.Format("Sent a PlaceOrder message with id:{0}", id));
            }
        }
    }
}
