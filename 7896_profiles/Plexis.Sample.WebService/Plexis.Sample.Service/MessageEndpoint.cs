using NServiceBus;
using log4net;


namespace Plexis.Sample.Service
{
	[EndpointName("Plexis.Sample.Service.Input")]
	public class MessageEndpoint : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	{

		public void Init()
		{

			log.Debug("Configuring endpoint for Plexis.Sample.Service...");

			Configure.With()
				.DefaultBuilder()
				.DisableTimeoutManager()
				.DisableRavenInstall()
				.MsmqTransport();

			log.Info("Endpoint configured for Plexis.Sample.Service");		
		}

		public static ILog log = LogManager.GetLogger(typeof(MessageEndpoint));
	}
}
