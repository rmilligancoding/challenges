namespace LendingApplication.UnitTests.Metrics
{
    using LendingApplication.Loans;
    using LendingApplication.Validators;

    public class LoanValidatorTests
    {
        private readonly LoanValidator _loanValidator;

        public LoanValidatorTests()
        {
            _loanValidator = new LoanValidator();
        }

        [Fact]
        public void GetMetrics_NullLoanApplication_ThrowsException()
        {
            //Arrange Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                _loanValidator.IsLoanApplicationValid(null));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'loanApplication')", exception.Message);
        }

        [Fact]
        public void GetMetrics_LoanApplicationAmountEqualsAssetValue_ThrowsException()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 800000, AssetValue = 80000, CreditScore = 500 };

            // Act
            var exception = Assert.Throws<Exception>(() =>
                _loanValidator.IsLoanApplicationValid(loanApplication));

            //Assert
            Assert.Equal("Loan amount must be less than asset value", exception.Message);
        }

        [Fact]
        public void GetMetrics_LoanApplicationAmountGreaterThanAssetValue_ThrowsException()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 900000, AssetValue = 80000, CreditScore = 500 };

            // Act
            var exception = Assert.Throws<Exception>(() =>
                _loanValidator.IsLoanApplicationValid(loanApplication));

            //Assert
            Assert.Equal("Loan amount must be less than asset value", exception.Message);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnRangeRuleMin_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 80000, AssetValue = 400000, CreditScore = 500 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnRangeRuleMax_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 2000000, AssetValue = 4000000, CreditScore = 500 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnUpperRangeRuleCreditScore_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 1200000, AssetValue = 2400000, CreditScore = 800 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnUpperRangeRuleLoanToValue_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 1200000, AssetValue = 1400000, CreditScore = 1000 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnLowerRangeRuleCreditScoreLower_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 1200000, CreditScore = 650 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanValidOnLowerRangeRuleCreditScoreLowerEquals_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 1200000, CreditScore = 750 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLoanValid_LoanValidOnLowerRangeRuleCreditScoreLowerGreater_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 1200000, CreditScore = 800 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnLowerRangeRuleCreditScoreMid_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 700000, CreditScore = 700 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanValidOnLowerRangeRuleCreditScoreMidEquals_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 700000, CreditScore = 800 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLoanValid_LoanValidOnLowerRangeRuleCreditScoreMidGreater_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 700000, CreditScore = 850 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnLowerRangeRuleCreditScoreUpper_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 600000, CreditScore = 800 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanValidOnLowerRangeRuleCreditScoreUpperEquals_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 600000, CreditScore = 900 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLoanValid_LoanValidOnLowerRangeRuleCreditScoreUpperGreater_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 600000, CreditScore = 950 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLoanValid_LoanInvalidOnLowerRangeRuleLoanToValue_Failure()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 500000, AssetValue = 510000 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLoanValid_LoanValid_Success()
        {
            //Arrange
            var loanApplication = new LoanApplication { Amount = 400000, AssetValue = 1100000, CreditScore = 800 };

            //Arrange
            var result = _loanValidator.IsLoanApplicationValid(loanApplication);

            //Assert
            Assert.True(result);
        }

    }
}
