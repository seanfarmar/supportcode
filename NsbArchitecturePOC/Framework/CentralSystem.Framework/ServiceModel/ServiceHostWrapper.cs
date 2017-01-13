namespace CentralSystem.Framework.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Service host wrapper
    /// </summary>
    public sealed class ServiceHostWrapper
    {

        #region Members

        /// <summary>
        /// Service host list
        /// </summary>
        private readonly List<ServiceHost> m_serviceHostList = new List<ServiceHost>();

        #endregion

        #region Methods

        /// <summary>
        /// Open service host
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <param name="baseAddresses">Base addresses</param>
        /// <returns>Current wrapper</returns>
        public ServiceHostWrapper RegisterOpen(System.Type serviceType, params Uri[] baseAddresses)
        {
            ServiceHost serviceHost = new ServiceHost(serviceType, baseAddresses);
            m_serviceHostList.Add(serviceHost);
            return this;
        }

        /// <summary>
        /// Open registered service host
        /// </summary>
        /// <returns>Current wrapper</returns>
        public ServiceHostWrapper Open()
        {
            foreach (ServiceHost serviceHost in m_serviceHostList)
            {
                serviceHost.Open();
            }
            return this;
        }

        /// <summary>
        /// Close service host
        /// </summary>
        public void Close()
        {
            foreach (ServiceHost serviceHost in m_serviceHostList.ToArray())
            {
                m_serviceHostList.Remove(serviceHost);
                if (serviceHost.State == CommunicationState.Opened)
                {
                    serviceHost.Close();
                }
                ((IDisposable)serviceHost).Dispose();
            }
        }

        #endregion

    }
}
