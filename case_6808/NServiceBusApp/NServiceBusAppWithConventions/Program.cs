namespace NServiceBusAppWithConventions
{
    using System;
    using System.Windows.Forms;
    using NServiceBus;
    using NServiceBus.Installation.Environments;

    internal static class Program
    {
        private static IBus _bus;

        private static IStartableBus _startableBus;

        public static IBus Bus
        {
            get { return _bus; }
        }

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
            _startableBus = Configure.With()
                 .DefaultBuilder()
                 .UnicastBus()
                 .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

            _bus = _startableBus.Start();
        }
    }
}