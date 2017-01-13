namespace CentralSystem.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NServiceBus;

    /// <summary>
    /// Marking interface for commands which should be connected to resource management
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
    public interface IResourceCommand : IMessage
    {

    }
}
