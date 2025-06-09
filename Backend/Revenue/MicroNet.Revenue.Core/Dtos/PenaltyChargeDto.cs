namespace MicroNet.Revenue.Core.Dtos
{
    public class PenaltyChargeDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime ChargedAt { get; set; }
    }
}
