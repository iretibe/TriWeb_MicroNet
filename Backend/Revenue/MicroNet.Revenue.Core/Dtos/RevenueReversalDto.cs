namespace MicroNet.Revenue.Core.Dtos
{
    public class RevenueReversalDto
    {
        public Guid Id { get; set; }
        public Guid OriginalTransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ReversedBy { get; set; } = default!;
        public DateTime ReversedAt { get; set; }
    }
}
