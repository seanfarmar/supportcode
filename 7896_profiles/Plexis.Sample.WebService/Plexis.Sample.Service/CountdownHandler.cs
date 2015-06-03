using System;
using NServiceBus;
using Plexis.Sample.Messages;
using log4net;

namespace Plexis.Sample.Service
{
	public class CountdownHandler : IHandleMessages<Countdown>
	{
		public IBus Bus { get; set; }

		
		public void Handle(Countdown message)
		{
			
			var ts = new DateTime(2015, 12, 18) - DateTime.Now;
			log.InfoFormat("{0} days until The Force Awakens comes to a theater near you!" , ts.Days);
			
		}

		public static ILog log = LogManager.GetLogger(typeof(CountdownHandler));

	}
}
