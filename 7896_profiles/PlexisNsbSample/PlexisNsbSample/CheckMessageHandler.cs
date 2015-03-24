using log4net;
using NHibernate;
using NServiceBus;

namespace PlexisNsbSample
{
	public class CheckMessageHandler : IHandleMessages<PlexisMessage>
	{
		public IBus Bus { get; set; }
		public ISession Session { get; set; }
	
		private readonly ILog log = LogManager.GetLogger(typeof(CheckMessageHandler));

		public void Handle(PlexisMessage message)
		{
			log.InfoFormat("Check Message Handler received {0}", message);
		}

   }
}