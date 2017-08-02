namespace CentralSystem.Messaging.Implementation.Mapping.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.FlowManager.DrawFlow;
    using FlowManagerMessageContracts = CentralSystem.Messaging.FlowManager.DrawFlow;
    using FlowManagerValueObjects = CentralSystem.DataObjects.ValueObjects.FlowManager.DrawFlow;

    /// <summary>
    /// Mapping builder from message to value object
    /// </summary>
    public sealed class DrawFlowHourlyActivityCommandMappingBuilder
    {

        #region Methods

        /// <summary>
        /// Execute mapping
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        public void ExecuteMapping(FlowManagerMessageContracts.BaseDrawFlowHourlyActivityCommand source, FlowManagerValueObjects.DrawFlowHourlyActivityCommand destination)
        {
            new DrawFlowActivityCommandMappingBuilder().ExecuteMapping(source, destination);

            destination.HourlyType = MapHourlyActivityTypes(source.HourlyType);
            destination.PreviousTimeIdentifier = source.PreviousTimeIdentifier;
            destination.CurrentTimeIdentifier = source.CurrentTimeIdentifier;
            destination.CDC = source.CDC;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Convert hourly type value
        /// </summary>
        /// <param name="sourceHourlyActivityType">Source value</param>
        /// <returns>Mapped value</returns>
        private FlowManagerValueObjects.HourlyActivityTypes MapHourlyActivityTypes(HourlyActivityTypes sourceHourlyActivityType)
        {
            //Default is no flag empty value
            FlowManagerValueObjects.HourlyActivityTypes result = FlowManagerValueObjects.HourlyActivityTypes.HourlyRegular;

            foreach (HourlyActivityTypes sourceItem in System.Enum.GetValues(typeof(HourlyActivityTypes)))
            {
                if (!sourceHourlyActivityType.HasFlag(sourceItem)) continue;
                switch (sourceItem)
                {
                    case HourlyActivityTypes.HourlyRegular:
                        //Default mapped value
                        break;
                    case HourlyActivityTypes.FirstTime:
                        result = result | FlowManagerValueObjects.HourlyActivityTypes.FirstTime;
                        break;
                    case HourlyActivityTypes.LastHourBeforeEOD:
                        result = result | FlowManagerValueObjects.HourlyActivityTypes.LastHourBeforeEOD;
                        break;
                    case HourlyActivityTypes.LastTime:
                        result = result | FlowManagerValueObjects.HourlyActivityTypes.LastTime;
                        break;
                    default:
                        throw new NotSupportedException(
                            string.Format("Not supported hourly type '{0}'", sourceHourlyActivityType.ToString()));
                }
            }

            return result;
        }

        #endregion

    }
}
