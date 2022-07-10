namespace LendingApplication.UnitTests
{
    using LendingApplication.Data;
    using LendingApplication.Loans;
    using LendingApplication.Metrics;
    using LendingApplication.Validators;
    using Moq;

    public class LendingPlatformTests : IDisposable
    {
        private readonly LendingPlatform _lendingPlatform;
        private readonly Mock<ILoanValidator> _loanValidator;
        private readonly Mock<ILoanStore> _loanStore;
        private readonly Mock<ILoanMetricsHelper> _loanMetricsHelper;

        public LendingPlatformTests()
        {
            _loanValidator = new Mock<ILoanValidator>(MockBehavior.Strict);
            _loanStore = new Mock<ILoanStore>(MockBehavior.Strict);
            _loanMetricsHelper = new Mock<ILoanMetricsHelper>(MockBehavior.Strict);

            _lendingPlatform = new LendingPlatform(_loanValidator.Object, _loanStore.Object, _loanMetricsHelper.Object);
        }

        public void Dispose()
        {
            _loanValidator.VerifyAll();
            _loanStore.VerifyAll();
            _loanMetricsHelper.VerifyAll();
        }

        [Fact]
        public void Ctor_NullLoanValidator_ThrowsException()
        {
            //Arrange Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new LendingPlatform(null, _loanStore.Object, _loanMetricsHelper.Object));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'loanValidator')", exception.Message);
        }

        [Fact]
        public void Ctor_NullLoanStore_ThrowsException()
        {
            //Arrange Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new LendingPlatform(_loanValidator.Object, null, _loanMetricsHelper.Object));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'loanStore')", exception.Message);
        }

        [Fact]
        public void Ctor_NullLoanMetricsHelper_ThrowsException()
        {
            //Arrange Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new LendingPlatform(_loanValidator.Object, _loanStore.Object, null));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'loanMetricsHelper')", exception.Message);
        }

        [Fact]
        public void WriteLoan_NullLoanApplication_ThrowsException()
        {
            //Arrange
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _lendingPlatform.WriteLoan(null));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'loanApplication')", exception.Message);
        }

        [Fact]
        public void WriteLoan_LoanApplicationProvided_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication();

            _loanValidator.Setup(l => l.IsLoanApplicationValid(loanApplication)).Returns(true);
            _loanStore.Setup(l => l.Loans.Add(It.IsAny<Loan>()));
            _loanMetricsHelper.Setup(l => l.GetMetrics(It.IsAny<LoanStatus>())).Returns(new LoanMetrics());

            //Act
            var metrics = _lendingPlatform.WriteLoan(loanApplication);

            //Assert
            Assert.NotNull(metrics);
        }
    }
}