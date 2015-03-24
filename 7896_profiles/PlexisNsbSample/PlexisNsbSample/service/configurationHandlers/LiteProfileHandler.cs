using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Hosting.Profiles;
using log4net;

namespace PlexisNsbSample
{
	public class LiteProfileHandler :
		IHandleProfile<Lite>,
		IHandleProfile<Integration>,
		IWantTheEndpointConfig
	{
		public IConfigureThisEndpoint Config { get; set; }
		private ILog log = LogManager.GetLogger(typeof(LiteProfileHandler));
		public void ProfileActivated()
		{
			if (Config is AsA_Service)
			{
				log.Info("Profile activated...");
				if(Configure.GetConfigSection<DBSubscriptionStorageConfig>() != null)
				{
					log.Debug("Using DBSubcriptionStorage");
					 Configure.Instance.DBSubcriptionStorage();
				}
				else
				{
					log.Debug("Using InMemorySubscriptionStorage");
					Configure.Instance.InMemorySubscriptionStorage();
				}
			}
		}
	}
}