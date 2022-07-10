namespace LendingApplication.Validators
{
    using LendingApplication.Extensions;
    using LendingApplication.Loans;

    public class LoanUpperRangeRule : IRule
    {
        private const decimal ruleAmount = 1000000;
        private const decimal ruleLoanToValue = 60;
        private const decimal ruleCreditScore = 950;

        /// <summary>Check whether the loan amount is valid</summary>
        /// <para name="loanApplication">The loan application.</para>
        /// <returns>Whether loan amount is valid</returns>
        public bool IsValid(LoanApplication loanApplication)
        {
            if (loanApplication.Amount >= ruleAmount)
            {
                if (loanApplication.CreditScore < ruleCreditScore)
                    return false;

                var loanToValue = loanApplication.Amount.LoanToValue(loanApplication.AssetValue);
                if (loanToValue > ruleLoanToValue)
                    return false;
            }

            return true;
        }

    }
}
