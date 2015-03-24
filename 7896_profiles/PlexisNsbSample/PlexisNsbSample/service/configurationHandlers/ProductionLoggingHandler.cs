using System.Configuration;
using System.IO;
using System.Xml;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using NServiceBus;

namespace PlexisNsbSample
{
	public class ProductionLoggingHandler : IConfigureLoggingForProfile<Production>
	{
		public void Configure(IConfigureThisEndpoint specifier)
		{
			AppenderSkeleton rf = null;

			var queue = specifier.ToString()
				.Remove(specifier.ToString()
				.LastIndexOf('.'));

			var pathToConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
				.FilePath;

			if(File.Exists(pathToConfig))
			{
				var config = new XmlDocument();
				config.Load(pathToConfig);
			}

			SetLoggingLibrary.Log4Net<RollingFileAppender>(null,
				a =>
				{
					a.CountDirection = 1;
					a.DatePattern = "yyyy-MM-dd";
					a.RollingStyle = RollingFileAppender.RollingMode.Composite;
					a.MaxFileSize = 1024 * 1024 * 2;
					a.MaxSizeRollBackups = 10;
					a.LockingModel = new FileAppender.MinimalLock();
					a.StaticLogFileName = true;
					a.File = Path.Combine("..\\..", "Logs", queue + ".log");
					a.AppendToFile = true;
					a.Layout = new PatternLayout("%d [%t] %-5p %c\n\t%m%n");

					rf = a;
				});

			var repo = LogManager.GetAllRepositories()[0];

			var nsbSerializer = (Logger)repo.GetLogger("NServiceBus.Serializers");
			var nhibernate = (Logger)repo.GetLogger("NHibernate");

			nsbSerializer.Level =
				rf.Threshold < Level.Warn
					? Level.Warn
					: rf.Threshold;
			nhibernate.Level =
				rf.Threshold < Level.Warn
					? Level.Warn
					: rf.Threshold;


		}
	}
}