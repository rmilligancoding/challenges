namespace LendingApplication.Metrics
{
    using LendingApplication.Loans;
    using System.Globalization;

    public class LoanMetrics
    {
        public LoanStatus LoanStatus { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AverageLoanToValue { get; set; }

        public int TotalNumberOfAcceptedApplicants { get; set; }

        public int TotalNumberOfDeclinedApplicants { get; set; }

        public int TotalNumberOfApplicants 
        { 
            get
            {
                return TotalNumberOfAcceptedApplicants + TotalNumberOfDeclinedApplicants;
            }
        }

        public string Summmary
        {
            get
            {
                return $"The loan status is {LoanStatus}. " +
                    $"Total value of loans {TotalAmount.ToString("C", new CultureInfo("en-GB"))} with an mean average Loan To Value of {AverageLoanToValue:0.##}%. " +
                    $"Total number of applicants {TotalNumberOfApplicants} with {TotalNumberOfAcceptedApplicants} accepted and {TotalNumberOfDeclinedApplicants} declined";
            }
        }
    }
}
