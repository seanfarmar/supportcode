namespace CentralSystem.DataObjects.ValueObjects.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Flow activity command definition
    /// </summary>
    public class FlowActivityCommand : BaseRequest 
    {

        #region Properties

        /// <summary>
        /// Flow instance ID
        /// </summary>
        public int FlowInstanceID { get; set; }

        /// <summary>
        /// Step instance ID
        /// </summary>
        public int StepInstanceID { get; set; }

        #endregion

    }
}
