using System.Configuration;
using NServiceBus;
using IsolationLevel = System.Transactions.IsolationLevel;


namespace Plexis.Sample.Service
{
	public static class RoleManager
	{
		public static void ConfigureRole(IConfigureThisEndpoint specified)
		{
			var nameSpace = "http://plexissample.com";

			var ns = ConfigurationManager.AppSettings["serializer.nameSpace"];

			if(!string.IsNullOrEmpty(ns))
				nameSpace = ns;

			if (specified is AsA_Service)
			{
				var uniConfig = Configure.Instance
					.XmlSerializer(nameSpace)
					.MsmqTransport()
						.IsTransactional(true)
						.IsolationLevel(IsolationLevel.ReadCommitted)
						.PurgeOnStartup(false)
					.UnicastBus()
						.ImpersonateSender(false)
						.DoNotAutoSubscribe();


				uniConfig.LoadMessageHandlers();

			}
		}
	}
}