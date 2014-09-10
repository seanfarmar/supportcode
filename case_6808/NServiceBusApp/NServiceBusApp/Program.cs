namespace NServiceBusApp
{
    using System;
    using System.Windows.Forms;

    using NServiceBus;
    using NServiceBus.Installation.Environments;

    internal static class Program
    {
        public static IBus Bus;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            InitBus();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void InitBus()
        {
            Configure.ScaleOut(s => s.UseSingleBrokerQueue());

            Configure.Serialization.Xml();
            Configure.Transactions.Enable();

            Bus = Configure.With()
                .DefaultBuilder()
                .PurgeOnStartup(true)
                .UnicastBus()
                .RunHandlersUnderIncomingPrincipal(false)
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}