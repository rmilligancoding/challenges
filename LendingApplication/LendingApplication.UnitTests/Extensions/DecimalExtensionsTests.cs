namespace LendingApplication.UnitTests.Extensions
{
    using LendingApplication.Extensions;

    public class DecimalExtensionsTests
    {
        [Fact]
        public void LoanToValue_AmountProvided_LoanToValueCalculated()
        {
            //Arrange
            decimal amount = 100000;
            decimal assetValue = 200000;

            // Act
            var result = amount.LoanToValue(assetValue);

            //Assert
            Assert.Equal(50, result);
        }
    }
}
