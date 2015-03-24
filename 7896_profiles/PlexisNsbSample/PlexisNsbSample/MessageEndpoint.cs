using System.Diagnostics;
using Castle.Core;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NServiceBus;
using log4net;
using Component = Castle.MicroKernel.Registration.Component;

namespace PlexisNsbSample.Input
{
	public class MessageEndpoint : IConfigureThisEndpoint, AsA_Service, IWantCustomInitialization, IOrderHandlers
	{
		private static ILog log;

		public void Init()
		{
			var container = new WindsorContainer();
			Configure.With()
				.CastleWindsorBuilder(container)
				.DisableRavenInstall()
				.MsmqTransport();

			log = LogManager.GetLogger(typeof (MessageEndpoint));

			container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();

			log.Debug("Message Endpoint initialized");
		}

		public void SpecifyOrder(Order order)
		{
			order.Specify(
				First<CheckMessageHandler>
					.Then<ProcessMessageHandler>());
		}
	}
}