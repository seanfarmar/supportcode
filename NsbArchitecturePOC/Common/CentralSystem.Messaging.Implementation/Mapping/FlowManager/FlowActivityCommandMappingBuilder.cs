namespace CentralSystem.Messaging.Implementation.Mapping.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FlowManagerMessageContracts = CentralSystem.Messaging.FlowManager;
    using FlowManagerValueObjects = CentralSystem.DataObjects.ValueObjects.FlowManager;

    /// <summary>
    /// Mapping builder from message to value object
    /// </summary>
    public sealed class FlowActivityCommandMappingBuilder
    {

        #region Methods

        /// <summary>
        /// Execute mapping
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        public void ExecuteMapping(FlowManagerMessageContracts.IFlowActivityCommand source, FlowManagerValueObjects.FlowActivityCommand destination)
        {
            new BaseMessageMappingBuilder().ExecuteMappingHeaders(source, destination);

            destination.FlowInstanceID = source.FlowInstanceID;
            destination.StepInstanceID = source.StepInstanceID;
        }

        /// <summary>
        /// Execute mapping
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        public void ExecuteMapping(FlowManagerMessageContracts.IFlowActivityCommand source, FlowManagerMessageContracts.IFlowActivityCommand destination)
        {
            new BaseMessageMappingBuilder().ExecuteMappingHeaders(source, destination);

            destination.FlowInstanceID = source.FlowInstanceID;
            destination.RootObjectID = source.RootObjectID;
            destination.RootObjectType = source.RootObjectType;

            destination.StepInstanceID = source.StepInstanceID;
        }

        #endregion

    }
}
