namespace LendingApplication.Metrics
{
    using LendingApplication.Loans;

    public interface ILoanMetricsHelper
    {
        LoanMetrics GetMetrics(LoanStatus loanStatus);
    }
}