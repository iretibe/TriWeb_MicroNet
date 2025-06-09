namespace MicroNet.Revenue.Core.Dtos
{
    public class CreateRevenueReversalDto
    {
        public Guid OriginalTransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ReversedBy { get; set; } = default!;
        public DateTime ReversedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
    }
}
