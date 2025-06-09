using MicroNet.Pos.Core.ValueObjects;

namespace MicroNet.Pos.Core.Entities
{
    public class Transaction : AggregateRoot
    {
        public string TransactionType { get; private set; } // BalanceEnquiry, Deposit, MobileDeposit
        public string AccountNumber { get; private set; }
        public string AccountName { get; private set; }
        public string Reference { get; private set; }
        public decimal Amount { get; private set; }
        public string PaymentChannel { get; private set; } // MicroNet, MobileMoney
        public string DepositorName { get; private set; }
        public string DepositorIdType { get; private set; }
        public string DepositorIdNumber { get; private set; }
        public string AgentCode { get; private set; }
        public string AgentPin { get; private set; }
        public string DestinationNetwork { get; private set; } = string.Empty;
        public string WalletNumber { get; private set; } = string.Empty;
        public string OTP { get; private set; } = string.Empty;
        public AuditInfo AuditInfo { get; private set; }

        private Transaction() { }

        public Transaction(string transactionType, string accountNumber, string accountName, string reference, decimal amount,
            string paymentChannel, string depositorName, string depositorIdType, string depositorIdNumber,
            string agentCode, string agentPin, string destinationNetwork, string walletNumber, string otp, string createdBy)
        {
            Id = Guid.NewGuid();
            TransactionType = transactionType;
            AccountNumber = accountNumber;
            AccountName = accountName;
            Reference = reference;
            Amount = amount;
            PaymentChannel = paymentChannel;
            DepositorName = depositorName;
            DepositorIdType = depositorIdType;
            DepositorIdNumber = depositorIdNumber;
            AgentCode = agentCode;
            AgentPin = agentPin;
            DestinationNetwork = destinationNetwork;
            WalletNumber = walletNumber;
            OTP = otp;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }
    }
}
