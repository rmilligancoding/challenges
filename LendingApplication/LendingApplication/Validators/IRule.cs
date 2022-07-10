namespace LendingApplication.Validators
{
    using LendingApplication.Loans;

    public interface IRule
    {
        bool IsValid(LoanApplication loanApplication);
    }
}