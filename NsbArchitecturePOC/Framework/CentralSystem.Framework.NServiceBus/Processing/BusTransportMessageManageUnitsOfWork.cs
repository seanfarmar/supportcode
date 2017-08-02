namespace CentralSystem.Framework.NServiceBus.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Framework.NServiceBus.Consts;
    using CentralSystem.Framework.NServiceBus.Diagnostics;
    using global::NServiceBus.UnitOfWork;
    using NHibernate;
    using CustomPerformanceCounters = CentralSystem.Framework.NServiceBus.Diagnostics.CustomPerformanceCounters;

    /// <summary>
    /// Current units of work is responsible for managing of message processing:
    /// - Manage logger context
    /// - If connected - performance counters activation
    /// IMPORTANT:
    ///     Registration should be executed by BusConfigurationExtensions.
    ///     Registration of class in the container is DependencyLifecycle.InstancePerUnitOfWork.
    ///     The instance will be singleton for the duration of the unit of work. In practice
    ///     this means the processing of a single transport message.
    /// </summary>
    internal sealed class BusTransportMessageProcessingManageUnitsOfWork : IManageUnitsOfWork
    {

        #region Properties

        /// <summary>
        /// Bus transport message processing subscriber to performance counters
        /// </summary>
        public CustomPerformanceCounters.BusTransportMessageProcessingSubscriber BusTransportMessageProcessingSubscriber { get; set; }

        /// <summary>
        /// Bus transport message processing execution context
        /// </summary>
        public BusExecutionContext BusExecutionContext { get; set; }

        #endregion

        #region IManageUnitsOfWork Members

        /// <summary>
        /// Begin event.
        /// Don't need try-catch - In context of bug should be initiated standard SLR mechanism with FATALs.
        /// </summary>
        public void Begin()
        {
            if (BusExecutionContext != null)
            {
                //In context of bug should be initiated standard SLR mechanism with FATALs
                BusExecutionContext.BeginUnitOfWorks();
            }

            //If registered subscriber - notify
            if (BusTransportMessageProcessingSubscriber != null)
            {
                BusTransportMessageProcessingSubscriber.NotifyBeginProcess();
            }

        }

        /// <summary>
        /// End event.
        /// Don't need try-catch - In context of bug should be initiated standard SLR mechanism with FATALs.
        /// </summary>
        /// <param name="ex">Exception</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public void End(Exception ex = null)
        {
            //If registered subscriber - notify
            if (BusTransportMessageProcessingSubscriber != null)
            {
                //Don't need try-catch - internal implementation
                BusTransportMessageProcessingSubscriber.NotifyEndProcess(ex);
            }

            if (BusExecutionContext != null)
            {
                //Don't need try-catch - internal implementation of the context
                BusExecutionContext.EndUnitOfWorks();
            }

        }

        #endregion

    }
}
