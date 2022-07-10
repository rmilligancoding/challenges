namespace LendingApplication.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal LoanToValue(this decimal amount, decimal assetValue)
        {
            return amount / assetValue * 100;
        }
    }
}
