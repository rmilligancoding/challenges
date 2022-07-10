namespace LendingApplication.Data
{
    using LendingApplication.Loans;

    public interface ILoanStore
    {
        IList<Loan> Loans { get; set; }
    }
}