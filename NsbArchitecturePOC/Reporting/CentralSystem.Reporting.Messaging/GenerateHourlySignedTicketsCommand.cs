namespace CentralSystem.Reporting.Messaging
{
    using CentralSystem.Messaging.FlowManager;
    using NServiceBus;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CentralSystem.Messaging.FlowManager.DrawFlow;

    public class GenerateHourlySignedTicketsCommand : BaseDrawFlowHourlyActivityCommand, ICommand
    {      
    }
}
