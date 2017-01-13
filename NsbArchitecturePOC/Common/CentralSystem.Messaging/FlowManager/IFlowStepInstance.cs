namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for identification of flow activity command
    /// </summary>
    public interface IFlowStepInstance : IFlowInstance
    {

        /// <summary>
        /// Step instance ID
        /// </summary>
        int StepInstanceID { get; set; }

    }
}
