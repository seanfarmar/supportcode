namespace Saga
{
    using System;
    using System.Collections.Generic;

    public class LoanInfo
    {
        public virtual Guid Id { get; set; }
        public virtual Int64 LoanId { get; set; }
        public virtual List<RailTransaction> Rails { get; set; }
    } 
}