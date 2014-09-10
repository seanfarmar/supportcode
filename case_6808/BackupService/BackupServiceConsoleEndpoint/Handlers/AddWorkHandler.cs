namespace Slb.BackupServiceConsoleEndpoint.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using NServiceBus;

    using Slb.Messages;

    public partial class AddWorkHandler : IHandleMessages<IAddWork>
    {
        public IBus Bus { get; set; }
        
        public void Handle(IAddWork message)
        {
            Console.WriteLine("IAddWork handled.");
        }
    }
}
