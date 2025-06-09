namespace MicroNet.Revenue.Core.Dtos
{
    public class CreateInterestDistributionDto
    {
        public DateTime PeriodStart { get; set; } = default!;
        public DateTime PeriodEnd { get; set; } = default!;
        public decimal TotalInterest { get; set; }
        public List<CreateAccountShareDto> DistributedTo { get; set; } = new();
        public string CreatedBy { get; set; } = default!;
        public DateTime DistributedAt { get; set; }
    }
}
