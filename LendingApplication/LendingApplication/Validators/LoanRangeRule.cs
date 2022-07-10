namespace LendingApplication.Validators
{
    using LendingApplication.Loans;

    public class LoanRangeRule : IRule
    {
        private const decimal ruleAmountMin = 100000;
        private const decimal ruleAmountMax = 1500000;

        /// <summary>Check whether the loan amount is valid</summary>
        /// <para name="loanApplication">The loan application.</para>
        /// <returns>Whether loan amount is valid</returns>
        public bool IsValid(LoanApplication loanApplication)
        {
            if (loanApplication.Amount < ruleAmountMin || loanApplication.Amount > ruleAmountMax)
                return false;

            return true;
        }

    }
}
