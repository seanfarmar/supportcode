namespace PayrollGenerator.Messages.Response
{
    using System;

    public class ProcessPayResponse
    {
        public Guid ProcessId { get; set; }
        public int EmployeeId { get; set; }
    }
}