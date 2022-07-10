namespace LendingApplication.UnitTests.Metrics
{
    using LendingApplication.Data;
    using LendingApplication.Loans;
    using LendingApplication.Metrics;
    using Moq;

    public class LoanMetricsHelperTests : IDisposable
    {
        private readonly LoanMetricsHelper _loanMetricsHelper;
        private readonly Mock<ILoanStore> _loanStore;

        public LoanMetricsHelperTests()
        {
            _loanStore = new Mock<ILoanStore>(MockBehavior.Strict);

            _loanMetricsHelper = new LoanMetricsHelper(_loanStore.Object);
        }

        public void Dispose()
        {
            _loanStore.VerifyAll();
        }

        [Fact]
        public void Ctor_NullLoanStore_ThrowsException()
        {
            //Arrange Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new LoanMetricsHelper(null));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'loanStore')", exception.Message);
        }

        [Fact]
        public void GetMetrics_NullLoanApplication_ThrowsException()
        {
            //Arrange
            var loanStatus = LoanStatus.Accepted;

            var loans = new List<Loan>
            {
                new Loan 
                {
                    LoanStatus = LoanStatus.Accepted
                }
            };

            _loanStore.Setup(l => l.Loans).Returns(loans);

            //Arrange
            var exception = Assert.Throws<Exception>(() =>
                _loanMetricsHelper.GetMetrics(loanStatus));

            //Assert
            Assert.Equal("Loan application is null.", exception.Message);
        }

        [Fact]
        public void GetMetrics_UnexpectedLoanStatus_ThrowsException()
        {
            //Arrange
            var loanStatus = LoanStatus.Accepted;

            var loans = new List<Loan>
            {
                new Loan
                {
                    LoanStatus = LoanStatus.Unknown,
                    LoanApplication = new LoanApplication { Amount = 400000, AssetValue = 1100000, CreditScore = 800 }
                }
            };

            _loanStore.Setup(l => l.Loans).Returns(loans);

            //Arrange
            var exception = Assert.Throws<Exception>(() =>
                _loanMetricsHelper.GetMetrics(loanStatus));

            //Assert
            Assert.Equal("Loan status is an unexpected value.", exception.Message);
        }

        [Fact]
        public void GetMetrics_LoanStatusProvided_Success()
        {
            //Arrange
            var loanStatus = LoanStatus.Accepted;

            var loans = new List<Loan>
            {
                new Loan 
                { 
                    LoanStatus = LoanStatus.Declined,
                    LoanApplication = new LoanApplication { Amount = 80000, AssetValue = 400000, CreditScore = 500 } 
                },
                new Loan 
                {
                    LoanStatus = LoanStatus.Declined,
                    LoanApplication = new LoanApplication { Amount = 1200000, AssetValue = 1400000, CreditScore = 1000 }
                },
                new Loan 
                { 
                    LoanStatus = LoanStatus.Declined,
                    LoanApplication = new LoanApplication { Amount = 500000, AssetValue = 1200000, CreditScore = 600 }
                },
                new Loan 
                {
                    LoanStatus = LoanStatus.Accepted,
                    LoanApplication = new LoanApplication { Amount = 400000, AssetValue = 1100000, CreditScore = 800 }
                }
            };

            _loanStore.Setup(l => l.Loans).Returns(loans);

            //Act
            var metrics = _loanMetricsHelper.GetMetrics(loanStatus);

            //Assert
            Assert.NotNull(metrics);
            Assert.Equal(loanStatus, metrics.LoanStatus);
            Assert.Equal(2180000, metrics.TotalAmount);
            Assert.Equal("53.17", metrics.AverageLoanToValue.ToString("0.##"));
            Assert.Equal(1, metrics.TotalNumberOfAcceptedApplicants);
            Assert.Equal(3, metrics.TotalNumberOfDeclinedApplicants);
            Assert.Equal(loans.Count, metrics.TotalNumberOfApplicants);
            Assert.Equal("The loan status is Accepted. Total value of loans £2,180,000.00 with an mean average Loan To Value of 53.17%. Total number of applicants 4 with 1 accepted and 3 declined", metrics.Summmary);
        }
    }
}
