namespace CentralSystem.Messaging.Implementation.Mapping.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FlowManagerMessageContracts = CentralSystem.Messaging.FlowManager.EODFlow;
    using FlowManagerValueObjects = CentralSystem.DataObjects.ValueObjects.FlowManager.EODFlow;

    /// <summary>
    /// Mapping builder from message to value object
    /// </summary>
    public class EODFlowActivityCommandMappingBuilder
    {

        #region IMappingBuilder<BaseEODFlowActivityCommand,EODFlowActivityCommand> Members

        /// <summary>
        /// Execute mapping
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        public void ExecuteMapping(FlowManagerMessageContracts.BaseEODFlowActivityCommand source, FlowManagerValueObjects.EODFlowActivityCommand destination)
        {
            new FlowActivityCommandMappingBuilder().ExecuteMapping(source, destination);

            destination.CDC = source.CDC;
        }

        #endregion

    }
}
