using System;
using NServiceBus;
using PlexisNsbSample;

namespace PlexisBusPass
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

			var bus = Configure.With()
				.DefaultBuilder()
				.XmlSerializer()
				.MsmqTransport()
				.UnicastBus()
				.SendOnly();

			while (Console.ReadLine()!=null)
			{
				var queue = "PlexisNsbSample.input";
				var msg = new PlexisMessage();
				Console.WriteLine("{0} Sending {1} to {2}", DateTime.Now, msg, queue);
				bus.Send(queue, msg);
			}

		}
	}
}
