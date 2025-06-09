using MicroNet.Account.Core.ValueObjects;

namespace MicroNet.Account.Core.Entities
{
    public class Withdrawal : AggregateRoot
    {
        public string AccountNumber { get; private set; }
        public decimal Amount { get; private set; }
        public string PaymentMode { get; private set; } // Account, Cash, Wallet
        public string Reference { get; private set; }
        public string WalletNumber { get; private set; } = string.Empty;
        public string Network { get; private set; } = string.Empty;
        public AuditInfo AuditInfo { get; private set; }

        public Withdrawal()
        {
            
        }

        public Withdrawal(string account, decimal amount, string paymentMode, string reference, string createdBy)
        {
            Id = Guid.NewGuid();
            AccountNumber = account;
            Amount = amount;
            PaymentMode = paymentMode;
            Reference = reference;
            AuditInfo = new AuditInfo(createdBy);
        }

        public void SetMobileDetails(string wallet, string network)
        {
            WalletNumber = wallet;
            Network = network;
        }

        public void Approve(string approver) => AuditInfo.Approve(approver);
    }
}
