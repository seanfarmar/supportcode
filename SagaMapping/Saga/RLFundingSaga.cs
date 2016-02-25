namespace Saga
{
    using System;
    using NServiceBus.Saga;

    public class RLFundingSaga : Saga<RLFundingSagaData>,
        IAmStartedByMessages<NewLoanProcessed>
       
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RLFundingSagaData> mapper)
        {
            mapper.ConfigureMapping<NewLoanProcessed>(message => message.LoanId).ToSaga(sagaData => sagaData.LoanId);
        }

        /// <summary>
        ///Generate TransactionGroup ID
        ///Call to FeeCalculation service to calculate Retained Interest & Accrued Interest Fees
        ///w/Origination Fees, call to CT to collect Fees
        ///Call to CT to move funding money in GL account
        ///Based on ordered list of rails in Loan (fail to next in list), fund:
        ///MC (skip if card number is Wells Fargo or amount is over $10k)
        ///ACH
        ///If Funding fails completely:
        ///roll back GL account and RI and AI (keep Origination Fees)
        ///Fire Event of what happened with funding
        /// </summary>
        /// <param name="message"></param>
        public void Handle(NewLoanProcessed message)
        {
           // Data.LoanInfo = message.LoanInfo;
            Data.LoanId = message.LoanId;

            // append new items only
            foreach (var i in message.LoanInfo.Rails)
            {
                Data.Rails.Add(i);               
            }
            
            // Data.FundingTransactionGroupID = GenerateTransactionId();
            //AssessFees(Data.LoanInfo);
            //Fund(Data.LoanInfo);

            Console.WriteLine("Handeling NewLoanProcessed with LoanId: {0}", message.LoanId);
        }
    }
    //for each rail - how much was processed and why if 0, specific rail transaction ID
    //loan totally funded?
    //transaction group ID
    //
}
