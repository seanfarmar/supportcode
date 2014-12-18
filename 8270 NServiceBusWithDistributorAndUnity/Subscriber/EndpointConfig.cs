
namespace Subscriber
{
    using NServiceBus;
    using NServiceBus.Installation.Environments;
    using NServiceBus.ObjectBuilder.Unity;
    using System.Configuration;
    using System.Diagnostics;
    using NServiceBus.ObjectBuilder.Common.Config;
    
	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    [EndpointSLA("0:00:05")]
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public bool IsDistributor 
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["IsDistributor"]);
            }
        }

        public void Init()
        {
            Configure.With()
                .UnityBuilder()                 // does not work
                //.DefaultBuilder()             // works
                .UnicastBus()
                .MsmqSubscriptionStorage()      //Default is RavenDb
                .UseInMemoryTimeoutPersister()  //Default is RavenDb
                ;

            if (IsDistributor)
                Configure.With()
                    .RunMSMQDistributor(withWorker: true);  // Distributor and worker
            else
                Configure.With()
                    .EnlistWithMSMQDistributor();   // Worker
        }
    }
}
