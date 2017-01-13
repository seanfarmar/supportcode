namespace CentralSystem.Messaging.Implementation.Mapping.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FlowManagerMessageContracts = CentralSystem.Messaging.FlowManager.DrawFlow;
    using FlowManagerValueObjects = CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow;

    /// <summary>
    /// Mapping builder from message to value object
    /// </summary>
    public class DrawFlowEODActivityCommandMappingBuilder
    {

        #region Methods

        /// <summary>
        /// Execute mapping
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        public void ExecuteMapping(FlowManagerMessageContracts.BaseDrawFlowEODActivityCommand source, FlowManagerValueObjects.DrawFlowEODActivityCommand destination)
        {
            new DrawFlowActivityCommandMappingBuilder().ExecuteMapping(source, destination);

            destination.IsFirst = source.IsFirst;
            destination.CDC = source.CDC;
        }

        #endregion

    }
}
