namespace PayrollGenerator.Messages.Commands
{
    using System;

    public class ProcessPayCommand
    {
        public Guid ProcessId { get; set; }
        public int EmployeeId { get; set; }
    }
}