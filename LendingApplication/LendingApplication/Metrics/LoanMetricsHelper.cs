namespace LendingApplication.Metrics
{
    using LendingApplication.Loans;
    using LendingApplication.Data;
    using LendingApplication.Extensions;

    public class LoanMetricsHelper : ILoanMetricsHelper
    {
        private readonly ILoanStore _loanStore;

        public LoanMetricsHelper(ILoanStore loanStore)
        {
            _loanStore = loanStore ?? throw new ArgumentNullException(nameof(loanStore));
        }

        /// <summary>Gets metrics</summary>
        /// <para name="loanStatus">The loan status.</para>
        /// <returns>The metrics.</returns>
        public LoanMetrics GetMetrics(LoanStatus loanStatus)
        {
            decimal totalAmount = 0;
            decimal totalAssetValue = 0;
            int totalNumberOfAcceptedApplicants = 0;
            int totalNumberOfDeclinedApplicants = 0;

            foreach (var loan in _loanStore.Loans)
            {
                if (loan.LoanApplication == null) 
                    throw new Exception("Loan application is null.");

                totalAmount += loan.LoanApplication.Amount;
                totalAssetValue += loan.LoanApplication.AssetValue;

                switch (loan.LoanStatus)
                {
                    case LoanStatus.Accepted:
                    {
                        totalNumberOfAcceptedApplicants++;
                        break;
                    }
                    case LoanStatus.Declined:
                    {
                        totalNumberOfDeclinedApplicants++;
                        break;
                    }
                    default:
                    {
                        throw new Exception("Loan status is an unexpected value.");
                    }
                }
            }

            decimal averageLoanToValue = totalAmount.LoanToValue(totalAssetValue);

            return new LoanMetrics
            {
                LoanStatus = loanStatus,
                TotalAmount = totalAmount,
                AverageLoanToValue = averageLoanToValue,
                TotalNumberOfAcceptedApplicants = totalNumberOfAcceptedApplicants,
                TotalNumberOfDeclinedApplicants = totalNumberOfDeclinedApplicants
            };
        }
    }
}
