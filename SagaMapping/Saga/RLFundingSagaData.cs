namespace Saga
{
    using System;
    using System.Collections.Generic;
    using NServiceBus.Saga;

    public class RLFundingSagaData : ContainSagaData
    {

        [Unique]
        public virtual Int64 LoanId { get; set; }
        // creates a new list 
        public virtual IList<RailTransaction> Rails { get; set; } = new List<RailTransaction>();
        
        // public virtual LoanInfo LoanInfo { get; set; }
        // public virtual Int64 FundingTransactionGroupID { get; set; }
        // public virtual Decimal RetainedInterest { get; set; }
        // public virtual Decimal AccruedInterest { get; set; }
        // public virtual Decimal OriginationFee { get; set; }
        // public virtual string CTFeeChargeResponse { get; set; }
        // public virtual string CTGLResponse { get; set; }
        // public virtual List<IRailTransaction> RailResults { get; set; }
        // public virtual FundingResult Result { get; set; }
        // public virtual string CTFeeRollbackResponse { get; set; }
    }
}
