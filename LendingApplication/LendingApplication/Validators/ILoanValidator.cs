namespace LendingApplication.Validators
{
    using LendingApplication.Loans;

    public interface ILoanValidator
    {
        bool IsLoanApplicationValid(LoanApplication loanApplication);
    }
}