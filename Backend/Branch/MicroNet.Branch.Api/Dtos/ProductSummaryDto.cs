namespace MicroNet.Branch.Api.Dtos
{
    public class ProductSummaryDto
    {
        public LoanSummaryDto Loan { get; set; } = default!;
        public OtherProductSummaryDto OtherProduct { get; set; } = default!;
    }
}
