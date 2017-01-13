namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for identification of flow instance
    /// </summary>
    public interface IFlowInstance
    {

        #region Properties

        /// <summary>
        /// The brand ID
        /// </summary>
        short BrandID { get; set; }

        /// <summary>
        /// Flow instance ID
        /// </summary>
        int FlowInstanceID { get; set; }

        /// <summary>
        /// Root object type
        /// </summary>
        string RootObjectType { get; set; }

        /// <summary>
        /// New root object ID
        /// </summary>
        int RootObjectID { get; set; }

        #endregion

    }
}
