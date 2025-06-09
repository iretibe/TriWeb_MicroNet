namespace MicroNet.Account.Core.Dtos
{
    public class AccountTerminationDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public decimal AccountBalance { get; set; }
        public string Branch { get; set; } = default!;
        public string TerminationReason { get; set; } = default!;
        public string RequestedBy { get; set; } = default!;
        public string ApprovedBy { get; set; } = default!;
        public DateTime ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
