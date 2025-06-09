namespace MicroNet.Product.Core.Dtos
{
    public record LoanDto(Guid Id, string LoanCode, string LoanName, 
        decimal MaximumAmount, decimal PercentageOfSavings, decimal InterestRate,
        int RepaymentPeriod, int GracePeriod);
}
