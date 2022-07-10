namespace LendingApplication
{
    using LendingApplication.Loans;
    using LendingApplication.Data;
    using LendingApplication.Validators;
    using LendingApplication.Metrics;

    public class LendingPlatform
    {
        private readonly ILoanValidator _loanValidator;
        private readonly ILoanStore _loanStore;
        private readonly ILoanMetricsHelper _loanMetricsHelper;

        public LendingPlatform(ILoanValidator loanValidator, ILoanStore loanStore, ILoanMetricsHelper loanMetricsHelper)
        {
            _loanValidator = loanValidator ?? throw new ArgumentNullException(nameof(loanValidator));
            _loanStore = loanStore ?? throw new ArgumentNullException(nameof(loanStore));
            _loanMetricsHelper = loanMetricsHelper ?? throw new ArgumentNullException(nameof(loanMetricsHelper));
        }

        /// <summary>Write a loan</summary>
        /// <para name="loanApplication">The loan application.</para>
        /// <returns>The metrics.</returns>
        public LoanMetrics WriteLoan(LoanApplication loanApplication)
        {
            if (loanApplication == null) throw new ArgumentNullException(nameof(loanApplication));

            // create loan
            var loan = new Loan
            {
                LoanStatus = _loanValidator.IsLoanApplicationValid(loanApplication) ? LoanStatus.Accepted : LoanStatus.Declined,
                LoanApplication = loanApplication
            };

            // store loan
            _loanStore.Loans.Add(loan);

            // return metrics
            return _loanMetricsHelper.GetMetrics(loan.LoanStatus);
        }
    }
}
