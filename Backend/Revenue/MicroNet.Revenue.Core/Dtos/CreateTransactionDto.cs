namespace MicroNet.Revenue.Core.Dtos
{
    public class CreateTransactionDto
    {
        public string AccountNumber { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reference { get; set; } = default!;
        public string DepositorName { get; set; } = default!;
        public string IdType { get; set; } = default!;
        public string IdNumber { get; set; } = default!;
        public string DestinationType { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
