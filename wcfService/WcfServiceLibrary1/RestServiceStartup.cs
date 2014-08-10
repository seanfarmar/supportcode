namespace WcfServiceLibrary1
{
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using NServiceBus;

    public class RestServiceStartup : IWantToRunWhenBusStartsAndStops
    {
        private WebServiceHost _host;

        public void Start()
        {
            _host = new WebServiceHost(typeof (CreateProductService));
            _host.Open();
        }

        public void Stop()
        {
            if (null != _host && _host.State == CommunicationState.Opened)
                _host.Close();
        }
    }
}