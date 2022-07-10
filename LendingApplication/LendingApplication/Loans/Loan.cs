namespace LendingApplication.Loans
{
    public class Loan
    {
        public LoanStatus LoanStatus { get; set; }

        public LoanApplication? LoanApplication { get; set; }
    }
}
