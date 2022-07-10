namespace LendingApplication.Validators
{
    using LendingApplication.Extensions;
    using LendingApplication.Loans;

    public class LoanLowerRangeRule : IRule
    {
        private const decimal ruleAmount = 1000000;
        
        private const decimal ruleLoanToValueLower = 60;
        private const decimal ruleCreditScoreLower = 750;

        private const decimal ruleLoanToValueMid = 80;
        private const decimal ruleCreditScoreMid = 800;

        private const decimal ruleLoanToValueUpper = 90;
        private const decimal ruleCreditScoreUpper = 900;

        /// <summary>Check whether the loan amount is valid</summary>
        /// <para name="loanApplication">The loan application.</para>
        /// <returns>Whether loan amount is valid</returns>
        public bool IsValid(LoanApplication loanApplication)
        {
            if (loanApplication.Amount < ruleAmount)
            {
                var loanToValue = loanApplication.Amount.LoanToValue(loanApplication.AssetValue);

                if (loanToValue < ruleLoanToValueLower)
                {
                    return loanApplication.CreditScore >= ruleCreditScoreLower;
                }
                else if (loanToValue < ruleLoanToValueMid)
                {
                    return loanApplication.CreditScore >= ruleCreditScoreMid;
                }
                else if (loanToValue < ruleLoanToValueUpper)
                {
                    return loanApplication.CreditScore >= ruleCreditScoreUpper;
                }

                return false;
            }

            return true;
        }

    }
}
