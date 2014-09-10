namespace Slb.ConsumerWindowsFormsApplication
{
    using System;
    using System.Windows.Forms;

    using NServiceBus;

    using Slb.Messages;

    static class Program
    {
        public static IBus Bus;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitBus();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void InitBus()
        {
            //Bus = Configure.With().DefaultBuilder().UnicastBus().CreateBus().Start();

            Configure.ScaleOut(s => s.UseSingleBrokerQueue());

            Configure.Serialization.Xml();
            Configure.Transactions.Enable();

            Bus = Configure.With()
                           .DefaultBuilder()
                           .PurgeOnStartup(true)
                           .UnicastBus()
                           .RunHandlersUnderIncomingPrincipal(false)
                           .CreateBus()
                           .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());

            /*Bus = Configure.With()
                .DefaultBuilder()
                .PurgeOnStartup(false)
                .UnicastBus().ImpersonateSender(false).LoadMessageHandlers()
                .CreateBus()
                .Start();*/
        }

        /*public class CustomInit : INeedInitialization
        {
            public void Init()
            {
                Configure.With(@"D:\Dev\Slb\BackupService\BackupService\ConsumerWindowsFormsApplication\bin\Debug");

                Configure.With(new [] { typeof(AddWork), typeof(MyMessage) });
                
                Configure.With();
            }
        }*/
    }
}
