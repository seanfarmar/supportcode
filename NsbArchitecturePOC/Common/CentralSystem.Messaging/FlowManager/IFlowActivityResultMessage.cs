namespace CentralSystem.Messaging.FlowManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for identification of flow activity result message
    /// </summary>
    public interface IFlowActivityResultMessage : IFlowActivityCommand, IBaseResultMessage 
    {

    }
}
