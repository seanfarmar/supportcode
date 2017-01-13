namespace CentralSystem.Messaging.Implementation.Headers.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.FlowManager;
    using NServiceBus;
    using NServiceBus.MessageMutator;
    using NServiceBus.Unicast.Messages;

    /// <summary>
    /// Headers initialization for out coming messages.
    /// The component is responsible to provide special headers according to standard headers.
    /// This feature allows:
    /// - Find in the Timeout and Errors queues all relevant messages related to flow instance step: FlowInstanceStepID.
    /// </summary>
    internal sealed class BaseFlowStepInstanceBusHeadersInitializer : IMutateOutgoingTransportMessages
    {

        #region IMutateOutgoingTransportMessages Members

        /// <summary>
        /// Add header parameters to transport message
        /// </summary>
        /// <param name="logicalMessage">Logical message</param>
        /// <param name="transportMessage">Transport message</param>
        public void MutateOutgoing(LogicalMessage logicalMessage, TransportMessage transportMessage)
        {
            IFlowStepInstance baseFlowStepInstanceCommand = logicalMessage.Instance as IFlowStepInstance;
            if (baseFlowStepInstanceCommand != null)
            {
                transportMessage.Headers["NG.FM.FlowInstanceStepID"] = baseFlowStepInstanceCommand.StepInstanceID.ToString();
            }
        }

        #endregion

    }
}
