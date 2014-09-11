namespace Server.WebServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using NServiceBus;
    using NServiceBus.Hosting.Wcf;
    using NServiceBus.Logging;

    /// <summary>
    ///     Enable users to expose messages as WCF services
    /// </summary>
    public class CustomWcfManager: IWantToRunWhenBusStartsAndStops, INeedInitialization
    {
        private readonly List<ServiceHost> _hosts = new List<ServiceHost>();

        private WcfServiceHost _host;

        public void Init()
        {
        }
        
        private static Type GetContractType(Type t)
        {
            var args = t.BaseType.GetGenericArguments();

            return typeof(IWcfService<,>).MakeGenericType(args);
        }

        private static bool IsWcfService(Type t)
        {
            var args = t.GetGenericArguments();
            if (args.Length == 2)
                if (MessageConventionExtensions.IsMessageType(args[0]))
                {
                    var wcfType = typeof(WcfService<,>).MakeGenericType(args);
                    if (wcfType.IsAssignableFrom(t))
                        return true;
                }

            if (t.BaseType != null)
                return IsWcfService(t.BaseType) && !t.IsAbstract;

            return false;
        }

        private readonly ILog logger = LogManager.GetLogger(typeof(CustomWcfManager));

        /// <summary>
        /// Starts a <see cref="ServiceHost"/> for each found service. Defaults to <see cref="BasicHttpBinding"/> if
        /// no user specified binding is found
        /// </summary>
        public void Start()
        {
            foreach (var serviceType in Configure.TypesToScan.Where(t => !t.IsAbstract && IsWcfService(t)))
            {
                _host = new WcfServiceHost(serviceType);

                Binding binding = new BasicHttpBinding();

                if (Configure.Instance.Configurer.HasComponent<Binding>())
                    binding = Configure.Instance.Builder.Build<Binding>();

                _host.AddDefaultEndpoint(GetContractType(serviceType),
                                           binding
                                           , "");
                
                _hosts.Add(_host);

                logger.Debug("Initialising the WCF service: " + serviceType.AssemblyQualifiedName);
            
                _host.Open();            
            }
        }

        public void Stop()
        {
            _hosts.ForEach(h => h.Close());
        }
    }
}