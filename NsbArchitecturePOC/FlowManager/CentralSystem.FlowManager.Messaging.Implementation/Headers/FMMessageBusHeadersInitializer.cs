namespace CentralSystem.FlowManager.Messaging.Implementation.Headers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.FlowManager.Messaging.Implementation.Handlers.FlowProcessing.TimeoutObjects;
    using NServiceBus;
    using NServiceBus.MessageMutator;
    using NServiceBus.Unicast.Messages;

    /// <summary>
    /// Headers initialization for out coming messages.
    /// 
    /// The component is responsible to provide special headers for time reach events.
    /// This feature allows:
    /// - Find in the Timeout queue all ordered timer event according to specific step instance definition
    /// - In case if time reach event after all long retry will be moved to error queue, to identify problematic time based step instance definition.
    /// - Same reason for all activity execution timeout scenario.
    /// 
    /// For identification of standard messages - see description of headers initialization in common layer (namespace CentralSystem.Messaging.Implementation.Headers).
    /// </summary>
    internal sealed class FMMessageBusHeadersInitializer : IMutateOutgoingTransportMessages
    {

        #region IMutateOutgoingTransportMessages Members

        /// <summary>
        /// Add header parameters to transport message
        /// </summary>
        /// <param name="logicalMessage">Logical message</param>
        /// <param name="transportMessage">Transport message</param>
        public void MutateOutgoing(LogicalMessage logicalMessage, TransportMessage transportMessage)
        {
            BaseFlowActivityTimeoutIdentifier baseFlowActivityTimeoutIdentifier = logicalMessage.Instance as BaseFlowActivityTimeoutIdentifier;
            if (baseFlowActivityTimeoutIdentifier != null)
            {
                transportMessage.Headers["NG.FM.TimerID"] = baseFlowActivityTimeoutIdentifier.TimerIdentifier.ToString();
            }
        }

        #endregion

    }
}
