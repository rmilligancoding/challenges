namespace LendingApplication.Data
{
    using LendingApplication.Loans;

    public class LoanStore : ILoanStore
    {
        public IList<Loan> Loans { get; set; } = new List<Loan>();
    }
}
