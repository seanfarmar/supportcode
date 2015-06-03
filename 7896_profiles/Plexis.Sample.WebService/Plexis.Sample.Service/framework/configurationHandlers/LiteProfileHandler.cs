using NServiceBus;
using NServiceBus.Hosting.Profiles;
using log4net;

namespace Plexis.Sample.Service
{
	public class LiteProfileHandler :
		IHandleProfile<Lite>,
		IWantTheEndpointConfig
	{
		public IConfigureThisEndpoint Config { get; set; }
		private ILog log = LogManager.GetLogger(typeof(LiteProfileHandler));
		public void ProfileActivated()
		{
			if (Config is AsA_Service)
			{
				log.Info("Profile activated...");
				
					log.Debug("Using InMemorySubscriptionStorage");
					Configure.Instance.InMemorySubscriptionStorage();
				
			}
		}
	}
}