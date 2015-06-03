using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace Plexis.Sample.WebService
{
	public class Global : HttpApplication
	{
		public static IBus Bus { get; private set; }

		private void Application_Start(object sender, EventArgs e)
		{
			Bus = Configure.With()
				.DefineEndpointName("Plexis.Sample.WebService.Input")
				.Log4Net()
				.DefaultBuilder()
				.XmlSerializer("http://plexissample.com")
				.MsmqTransport()
				.UnicastBus()
					.LoadMessageHandlers()
				.CreateBus()
				.Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
		}

		void Application_End(object sender, EventArgs e)
		{
			
		}
	}
}
