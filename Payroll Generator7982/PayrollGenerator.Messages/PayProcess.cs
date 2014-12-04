namespace PayrollGenerator.Messages
{
    using System;
    using NServiceBus;
    using NServiceBus.Saga;

    public class PayProcessData : ContainSagaData
    {
        [Unique]
        public Guid ProcessId { get; set; }        
        public string Name { get; set; }
        public string VerificationCode { get; set; }
        public int PersonCount { get; set; }
    }

    public class PayProcess : ICommand
    {
        public Guid ProcessId { get; set; }
        public int Count { get; set; }
        public int EmployeeId { get; set; }
    }

    public class PayProcessResponse : IMessage
    {
        public Guid ProcessId { get; set; }
        public int NumberOfEmployeesInBatch { get; set; }
    }

    public class PayProcessStarter : ICommand
    {
        public Guid ProcessId { get; set; }
    }

    public class PayProcessEnder : ICommand
    {
        public Guid ProcessId { get; set; }
    }
}
