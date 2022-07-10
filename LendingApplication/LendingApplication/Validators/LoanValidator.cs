namespace LendingApplication.Validators
{
    using LendingApplication.Loans;

    public class LoanValidator : ILoanValidator
    {
        private readonly IEnumerable<IRule> rules;

        public LoanValidator()
        {
            rules = new IRule[]
            {
                new LoanRangeRule(),
                new LoanUpperRangeRule(),
                new LoanLowerRangeRule()
            };
        }

        /// <summary>Check whether the loan application is valid</summary>
        /// <para name="loanApplication">The loan application.</para>
        /// <returns>The metrics.</returns>
        public bool IsLoanApplicationValid(LoanApplication loanApplication)
        {
            if (loanApplication == null) throw new ArgumentNullException(nameof(loanApplication));
            if (loanApplication.Amount >= loanApplication.AssetValue) throw new Exception("Loan amount must be less than asset value");

            foreach (var rule in rules)
            {
                if (!rule.IsValid(loanApplication))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
