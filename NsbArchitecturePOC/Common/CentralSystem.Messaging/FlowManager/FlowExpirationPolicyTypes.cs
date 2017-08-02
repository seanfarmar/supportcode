namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Flow activity command expiration policy types
    /// </summary>
    public enum FlowExpirationPolicyTypes
    {

        /// <summary>
        /// Default policy scenario (configuration parameter "DefaultFlowActivityCommandExpirationPolicyInMin" in the WorkFlows database)
        /// </summary>
        Default = 0,

        /// <summary>
        /// Canceled expiration policy
        /// </summary>
        Canceled = 1,
        
        /// <summary>
        /// Configuration property with time interval (configuration parameter "[PropertyName]FlowActivityCommandExpirationPolicyInMin" in the WorkFlows database)
        /// </summary>
        ConfigurationPropertyWithTimeInterval = 2,

    }
}
