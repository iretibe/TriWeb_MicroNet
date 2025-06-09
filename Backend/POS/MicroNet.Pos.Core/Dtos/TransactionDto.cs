namespace MicroNet.Pos.Core.Dtos
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string TransactionType { get; set; } = default!; // BalanceEnquiry, Deposit, MobileDeposit
        public string AccountNumber { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public string Reference { get; set; } = default!;
        public decimal Amount { get; set; }
        public string PaymentChannel { get; set; } = default!; // MicroNet, MobileMoney
        public string DepositorName { get; set; } = default!;
        public string DepositorIdType { get; set; } = default!;
        public string DepositorIdNumber { get; set; } = default!;
        public string AgentCode { get; set; } = default!;
        public string AgentPin { get; set; } = default!;
        public string DestinationNetwork { get; set; } = string.Empty;
        public string WalletNumber { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
