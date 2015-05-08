using System.Configuration;
using System.Web;
using System.Web.Mvc;
using log4net.Core;
using NServiceBus;
using NServiceBus.Installation.Environments;
using Raven.Client;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ProblemTest
{
    public sealed class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(x =>
            {
                x.LookForRegistries();
            });
            

            //NSB            
            //FillAllPropertiesOfType<IDocumentSession>();            

            //NServiceBus
            //ForSingletonOf<IBus>().Use(
            //NServiceBus.Configure.With()
            //.StructureMapBuilder()
            //.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Command"))
            //.DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Event"))
            //.DefiningMessagesAs(t => t.Namespace == "Messages")
            //.RavenPersistence("RavenDB")
            //.UseTransport<ActiveMQ>()
            //.DefineEndpointName("IS.Argus.Core.Web")
            //.PurgeOnStartup(true)
            //.UnicastBus()
            //.CreateBus()
            //.Start(() => NServiceBus.Configure.Instance
            //.ForInstallationOn<Windows>()
            //.Install())
            //);            

            //Web             
            For<HttpContextBase>().Use(() => HttpContext.Current == null ? null : new HttpContextWrapper(HttpContext.Current));
            For<ModelBinderMappingDictionary>().Use(GetModelBinders());
            For<IModelBinderProvider>().Use<StructureMapModelBinderProvider>();
            For<IFilterProvider>().Use<StructureMapFilterProvider>();                                    
        }

        private static ModelBinderMappingDictionary GetModelBinders()
        {
            var binders = new ModelBinderMappingDictionary();
            return binders;
        }
    }
}