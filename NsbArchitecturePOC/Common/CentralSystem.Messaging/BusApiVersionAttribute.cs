namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Marking attribute to define API Version for all message contracts in the assembly. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class BusApiVersionAttribute : System.Attribute
    {

        public BusApiVersionAttribute(string apiVersion)
        {
            ApiVersion = apiVersion;
        }

        public string ApiVersion { get; private set; }

    }
}
