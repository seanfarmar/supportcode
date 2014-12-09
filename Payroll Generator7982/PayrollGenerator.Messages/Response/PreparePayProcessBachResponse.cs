namespace PayrollGenerator.Messages.Response
{
    using System;
    using System.Collections.Generic;
    using NServiceBus;

    public class PreparePayProcessBachResponse : IMessage
    {
        public Guid ProcessId { get; set; }
        public int NumberOfEmployeesInBatch { get; set; }
        public List<int> EmployeeIdList { get; set; } // list of employee ids to process 
    }
}