namespace PayrollGenerator.Messages.Commands
{
    using System;

    public class PreparePayProcessBachCommand
    {
        public Guid ProcessId { get; set; }
        public int Count { get; set; }
        public int EmployeeId { get; set; }
    }
}