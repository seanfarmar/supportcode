namespace CentralSystem.FlowManager.Messaging.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NServiceBus;
    using CentralSystem.FlowManager.Messaging.Activities;

    /// <summary>
    /// Startup class 
    /// </summary>
    public sealed class Startup : IWantToRunWhenBusStartsAndStops
    {

        public IBus Bus { get; set; }

        #region IWantToRunWhenBusStartsAndStops Members

        /// <summary>
        /// Start application
        /// </summary>
        public void Start()
        {
            System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// Stop application
        /// </summary>
        public void Stop()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Unhandled exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Exception</param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //CentralLogger.Fatal(
            //    CommonMessages.INT_APP_DOMAIN_UNHANDLED_EXCEPTION_CODE,
            //    CommonMessageCodes.INT_APP_DOMAIN_UNHANDLED_EXCEPTION_CODE,
            //    e.ExceptionObject as Exception);

            //Give time to process logging error if possible
            System.Threading.Thread.Sleep(100);
        }

        #endregion

    }
}
