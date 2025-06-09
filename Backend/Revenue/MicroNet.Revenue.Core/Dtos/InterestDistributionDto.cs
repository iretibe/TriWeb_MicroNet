namespace MicroNet.Revenue.Core.Dtos
{
    public class InterestDistributionDto
    {
        public Guid Id { get; set; }
        public string PeriodStart { get; set; } = default!;
        public string PeriodEnd { get; set; } = default!;
        public decimal TotalInterest { get; set; }
        public List<AccountShareDto> DistributedTo { get; set; } = new();
        public string CreatedBy { get; set; } = default!;
        public DateTime DistributedAt { get; set; }
    }
}
