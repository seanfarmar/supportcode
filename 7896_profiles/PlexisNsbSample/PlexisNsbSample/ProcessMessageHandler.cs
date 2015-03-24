using log4net;
using NHibernate;
using NServiceBus;

namespace PlexisNsbSample
{
	public class ProcessMessageHandler : IHandleMessages<PlexisMessage>
	{
		public IBus Bus { get; set; }
		public ISession Session { get; set; }
	
		private readonly ILog log = LogManager.GetLogger(typeof(ProcessMessageHandler));

		public void Handle(PlexisMessage message)
		{
			log.InfoFormat("Process Message Handler received {0}", message);
		}

   }
}