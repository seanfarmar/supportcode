namespace Saga
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus.Saga;

    public class RailTransaction
    {
        [Unique]
        public virtual Guid Id { get; set; }
        public virtual int RailID { get; set; }
        public virtual int Priority { get; set; }
        public virtual Decimal Amount { get; set; }
    }

    public interface IRT
    {
        Task<RailResult> Fund(RailTransaction rTrans, Decimal Amount);
        bool BusinessValidate(RailTransaction rTrans);
    }

    public class RailResult
    {
        //needed for nhibernate - not used by us
        public virtual Guid Id { get; set; }

        public virtual Decimal AmountFunded { get; set; }
        public virtual string RailTransactionID { get; set; }
        public virtual RailResultType FundResult { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime ProcessedAt { get; set; }

    }
    public enum RailResultType
    {
        NotAttempted,
        Success,
        Skipped, //e.g. CC amount > $10k, card numbers are Wells Fargo
        Failed
    }
}
