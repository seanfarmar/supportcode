namespace Saga
{
    using System;
    using System.Collections.Generic;
    using NHibernate.Cfg;
    using NServiceBus;
    using NServiceBus.Persistence;
    using Environment = NHibernate.Cfg.Environment;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            // NServiceBus provides the following durable storage options
            // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
            // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();

            // If you don't need a durable storage you can also use, configuration.UsePersistence<InMemoryPersistence>();
            // more details on persistence can be found here: http://docs.particular.net/nservicebus/persistence-in-nservicebus

            //Also note that you can mix and match storages to fit you specific needs. 

            Configuration nhConfiguration = new Configuration();

            nhConfiguration.SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            nhConfiguration.SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.Sql2008ClientDriver");
            nhConfiguration.SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2008Dialect");
            nhConfiguration.SetProperty(Environment.ConnectionString,
                @"Data Source=.\SqlExpress;Initial Catalog=NHSagaFinder;Integrated Security=True");

            //http://docs.particular.net/nservicebus/persistence-order
            configuration.UsePersistence<NHibernatePersistence>()
                .UseConfiguration(nhConfiguration);
        }
    }

    public class Bootstrap : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            List<RailTransaction> railsList = new List<RailTransaction>();

            railsList.Add(new RailTransaction {Amount = 1000, Priority = 1, RailID = 100}); 

            var loanId = new Random().Next();
            var loanInfo = new LoanInfo {LoanId = loanId, Rails = railsList };
            var msg = new NewLoanProcessed {LoanId = loanId, LoanInfo = loanInfo};

            Bus.SendLocal(msg);
        }

        public void Stop()
        {
            // throw new NotImplementedException();
        }
    }
}
