namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.FlowManager;
    using NServiceBus;

    /// <summary>
    /// Flow manager activity attribute to configure expiration policy for activity commands
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class FlowExpirationPolicyAttribute : System.Attribute
    {

        #region Constructors

        /// <summary>
        ///  Main constructor. Two options:
        ///  - Default - expiration configuration according to configuration parameter "DefaultFlowActivityCommandExpirationPolicyInMin" in the WorkFlows database
        ///  - Canceled - don't activate expiration policy for current activity command
        /// </summary>
        /// <param name="expirationPolicyType">Expiration policy type</param>
        public FlowExpirationPolicyAttribute(FlowExpirationPolicyTypes expirationPolicyType)
        {
            if (expirationPolicyType == FlowExpirationPolicyTypes.ConfigurationPropertyWithTimeInterval)
            {
                throw new ArgumentException("Expected defined configuration property name");
            }
            ExpirationPolicyType = expirationPolicyType;
        }

        /// <summary>
        ///  Constructor for definition of parameter name in the WorkFlows database according to pattern "[PropertyName]FlowActivityCommandExpirationPolicyInMin"
        ///  for activation of expiration policy.
        /// </summary>
        /// <param name="expirationPolicyType">Configuration property name</param>
        public FlowExpirationPolicyAttribute(string configurationPropertyName)
        {
            if (string.IsNullOrEmpty(configurationPropertyName))
            {
                throw new ArgumentNullException("configurationPropertyName");
            }
            ConfigurationPropertyName = configurationPropertyName;
            ExpirationPolicyType = FlowExpirationPolicyTypes.ConfigurationPropertyWithTimeInterval;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Expiration policy type
        /// </summary>
        public FlowExpirationPolicyTypes ExpirationPolicyType { get; private set; }

        /// <summary>
        /// Configuration property name
        /// </summary>
        public string ConfigurationPropertyName { get; private set; }

        #endregion

    }
}
