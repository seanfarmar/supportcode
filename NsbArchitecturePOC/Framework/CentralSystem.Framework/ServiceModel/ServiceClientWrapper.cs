namespace CentralSystem.Framework.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ServiceModel;

    /// <summary>
    /// Define interface to support for unit testing and dependencies
    /// </summary>
    /// <typeparam name="TServiceContract">Service contract</typeparam>
    public interface IServiceClientWrapper<TServiceContract> : IDisposable where TServiceContract : class
    {
        /// <summary>
        /// Channel
        /// </summary>
        TServiceContract Channel { get; }
    }

    /// <summary>
    /// Helper class to created dynamic WCF client proxies without creation of class in the code according to service contract.
    /// Dispose design pattern - https://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.100).aspx .
    /// No reason in the synchronization.
    /// </summary>
    /// <typeparam name="TServiceContract">Service contract</typeparam>
    public sealed class ServiceClientWrapper<TServiceContract> : IServiceClientWrapper<TServiceContract> where TServiceContract : class
    {

        #region Members

        private readonly string m_endPointConfigurationName;

        ChannelFactory<TServiceContract> m_factory;
        private TServiceContract m_channel;

        private bool m_disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="endPointConfigurationName">Endpoint configuration name</param>
        public ServiceClientWrapper(string endPointConfigurationName)
        {
            m_disposed = false;
            this.m_endPointConfigurationName = endPointConfigurationName;
        }

        /// <summary>
        /// Code finalization
        /// </summary>
        ~ServiceClientWrapper()
        {
            Dispose(false);
        }

        #endregion

        #region IServiceClientWrapper<TServiceContract> Members

        /// <summary>
        /// Activate channel according after first calling 
        /// </summary>
        public TServiceContract Channel
        {
            get
            {
                if (m_disposed)
                {
                    throw new ObjectDisposedException("Resource ServiceWrapper<" + typeof(TServiceContract) + "> has been disposed");
                }

                if (m_factory == null)
                {
                    m_factory = new ChannelFactory<TServiceContract>(this.m_endPointConfigurationName);
                }

                if (m_channel == null)
                {
                    m_channel = m_factory.CreateChannel();
                }

                return m_channel;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Dispose pattern implementation
        /// </summary>
        /// <param name="disposing">Flag to dispose</param>
        private void Dispose(bool disposing)
        {
            if (m_disposed) return;

            if (disposing)
            {
                //Release managed resources
                //No reason in the synchronization
                if (m_channel != null)
                {
                    CloseCommunicationObject((IClientChannel)m_channel);
                    m_channel = null;
                }
                if (m_factory != null)
                {
                    CloseCommunicationObject(m_factory);
                    m_factory = null;
                }
            }

            // Release unmanaged resources

            m_disposed = true;

        }

        /// <summary>
        /// Close communication object.
        /// https://relentlessdevelopment.wordpress.com/2010/01/17/closing-a-wcf-client-the-proper-way/
        /// </summary>
        /// <param name="communicationObject">Communication object</param>
        private void CloseCommunicationObject(ICommunicationObject communicationObject)
        {
            if (communicationObject == null) return;

            try
            {
                communicationObject.Close();
            }
            catch
            {
                communicationObject.Abort();
            }

        }

        #endregion

    }
}
