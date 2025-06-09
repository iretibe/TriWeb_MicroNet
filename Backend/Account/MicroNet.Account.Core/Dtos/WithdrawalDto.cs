namespace MicroNet.Account.Core.Dtos
{
    public class WithdrawalDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reference { get; set; } = default!;
        public string ModeOfPayment { get; set; } = default!; // Account, Cash, Wallet
        public string WalletNumber { get; set; } = default!;
        public string Network { get; set; } = default!;
        public string RequestedBy { get; set; } = default!;
        public string ApprovedBy { get; set; } = default!;
        public DateTime ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
