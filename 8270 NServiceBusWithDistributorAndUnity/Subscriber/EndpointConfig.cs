
namespace Subscriber
{
    using System.Configuration;
    using NServiceBus;
    using NServiceBus.Installation.Environments;
    using NServiceBus.ObjectBuilder.Unity;
    using System.Diagnostics;
    using NServiceBus.ObjectBuilder.Common.Config;
    
	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    // [EndpointSLA("0:00:05")]
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .UnityBuilder() // does not work
                //.DefaultBuilder()             // works
                .UnicastBus()
                .MsmqSubscriptionStorage() //Default is RavenDb
                .UseInMemoryTimeoutPersister();  //Default is RavenDb

            //if (IsDistributor)
            //    Configure.With()
            //        .RunMSMQDistributor(withWorker: true);  // Distributor and worker
            //else
            //    Configure.With()
            //        .EnlistWithMSMQDistributor();   // Worker
        }
    }

    //public class CustomeConfiguration : IWantToRunBeforeConfigurationIsFinalized
    //{

    //    public bool IsDistributor
    //    {
    //        get
    //        {
    //            return bool.Parse(ConfigurationManager.AppSettings["IsDistributor"]);
    //        }
    //    }

    //    public void Run()
    //    {
    //        if (IsDistributor)
    //            NServiceBus.Configure.With()
    //                .RunMSMQDistributor(withWorker: true);  // Distributor and worker
    //        else
    //            NServiceBus.Configure.With()
    //                .EnlistWithMSMQDistributor();
    //    }
    //}
}
