namespace Saga
{
    using System;
    using NServiceBus;

    public class NewLoanProcessed : IMessage
    {
        public Int64 LoanId { get; internal set; }
        public LoanInfo LoanInfo { get; set; }
    }
}