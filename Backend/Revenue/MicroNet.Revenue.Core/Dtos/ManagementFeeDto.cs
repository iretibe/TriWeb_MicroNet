namespace MicroNet.Revenue.Core.Dtos
{
    public class ManagementFeeDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;
        public string FeeType { get; set; } = default!; // e.g. "Percentage", "Flat"
        public decimal FeeValue { get; set; }
        public decimal CalculatedAmount { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime ChargedAt { get; set; }
    }
}
