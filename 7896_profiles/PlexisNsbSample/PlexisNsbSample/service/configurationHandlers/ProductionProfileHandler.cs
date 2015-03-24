using NServiceBus.Hosting.Profiles;
using log4net;
using NServiceBus;

namespace PlexisNsbSample
{
	public class ProductionProfileHandler :
		IHandleProfile<Production>,
		IWantTheEndpointConfig
	{
		private ILog log = LogManager.GetLogger(typeof(ProductionProfileHandler));

		public IConfigureThisEndpoint Config { get; set; }

		public void ProfileActivated()
		{
			log.Info("Profile activated...");
			if (Config is AsA_Service)
			{
				Configure.Instance.InMemorySubscriptionStorage();
			}
		}
	}
}