namespace SelfHost
{
	using System;
	using NServiceBus;
	using NServiceBus.Installation.Environments;

	class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
             Console.WriteLine("==========================================================================");
            }
        }
    }

    public class EndpoitConfig : IConfigureThisEndpoint, IWantCustomInitialization
    {
        public IBus Bus { get; set; }

        public void Init()
        {
            Configure.Transactions.Disable();
            Bus = Configure.With()
                .Log4Net()
                .DefaultBuilder()
                .UseTransport<Msmq>()
                .MsmqSubscriptionStorage()
                .DisableTimeoutManager()
                .PurgeOnStartup(false)
                .UnicastBus()
                .CreateBus()
                .Start(() =>
                Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}
