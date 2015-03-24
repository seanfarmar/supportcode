using System;
using System.Configuration;
using System.Data;
using System.Transactions;
using NServiceBus;
using IsolationLevel = System.Transactions.IsolationLevel;


namespace PlexisNsbSample
{
	public static class RoleManager
	{
		public static void ConfigureRole(IConfigureThisEndpoint specified)
		{
			var nameSpace = "http://plexisweb.com";

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


				if (specified is IOrderHandlers)
					(specified as IOrderHandlers).SpecifyOrder(new Order(uniConfig));
				else
					uniConfig.LoadMessageHandlers();

			}
		}
	}
}