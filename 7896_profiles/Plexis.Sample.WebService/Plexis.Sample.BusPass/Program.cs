using System;
using NServiceBus;

namespace Plexis.Sample.BusPass
{
	public class Program
	{
		static void Main(string[] args)
		{
			
			Console.WriteLine("Configuring BusPass...");
			var bus = Configure.With()
				.DefaultBuilder()
				.XmlSerializer()
				.MsmqTransport()
				.UnicastBus()
				.SendOnly();

			Console.WriteLine("BusPass Configuration Complete");

			Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");
			
			while (Console.ReadLine() != null)
			{
				var queue = "Plexis.Sample.WebService.Input";
				var msg = new Messages.Countdown();
				Console.WriteLine("{0} Sending {1} to WebService Queue @ {2}", DateTime.Now, msg, queue);
				bus.Send(queue, msg);
			}
		}
	}
}
