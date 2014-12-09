namespace PayrollGenerator.Saga
{
    using System;
    using System.Collections.Generic;
    using NServiceBus.Saga;

    public class PayProcessData : ContainSagaData
    {
        [Unique]
        public Guid ProcessId { get; set; }

        public string Name { get; set; }
        public string VerificationCode { get; set; }
        public int PersonCount { get; set; }
        public bool BatchReady { get; set; }
        public List<int> EmployeeIdList { get; set; }
        public List<int> EmployeeCompleteIdList { get; set; }
    }
}