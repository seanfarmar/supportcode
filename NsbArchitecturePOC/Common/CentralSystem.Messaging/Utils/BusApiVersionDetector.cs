namespace CentralSystem.Messaging.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Bus API version detector for message
    /// </summary>
    public sealed class BusApiVersionDetector
    {

        /// <summary>
        /// Apply bus API version in the message
        /// </summary>
        /// <param name="baseMessage">Base message</param>
        public void Apply(IBaseMessage baseMessage)
        {
            baseMessage.APIVersion = Detect(baseMessage.GetType());
        }

        /// <summary>
        /// Detect bus API version for message type
        /// </summary>
        /// <param name="messageType">Message type</param>
        /// <returns>Detected API version</returns>
        public string Detect(Type messageType)
        {
            // Class level
            BusApiVersionAttribute resultBusApiVersionAttribute = (BusApiVersionAttribute)
                Attribute.GetCustomAttribute(messageType, typeof(BusApiVersionAttribute), true);

            if (resultBusApiVersionAttribute == null)
            {
                // Custom assembly level
                resultBusApiVersionAttribute = (BusApiVersionAttribute)
                    Attribute.GetCustomAttribute(messageType.Assembly, typeof(BusApiVersionAttribute), false);

            }

            if (resultBusApiVersionAttribute == null)
            {
                // Common messaging assembly level
                resultBusApiVersionAttribute = (BusApiVersionAttribute)
                    Attribute.GetCustomAttribute(typeof(IBaseMessage).Assembly, typeof(BusApiVersionAttribute), false);

            }

            if (resultBusApiVersionAttribute == null)
            {
                throw new NotSupportedException("Not supported Bus API Version Attribute in the system");
            }

            return resultBusApiVersionAttribute.ApiVersion;

        }

    }
}
