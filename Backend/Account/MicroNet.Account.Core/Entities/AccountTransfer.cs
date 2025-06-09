using MicroNet.Account.Core.ValueObjects;

namespace MicroNet.Account.Core.Entities
{
    public class AccountTransfer : AggregateRoot
    {
        public AccountDetails SourceAccount { get; private set; }
        public string DestinationBranch { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public AccountTransfer()
        {
            
        }

        public AccountTransfer(AccountDetails sourceAccount, string destinationBranch, string createdBy)
        {
            Id = Guid.NewGuid();
            SourceAccount = sourceAccount;
            DestinationBranch = destinationBranch;
            AuditInfo = new AuditInfo(createdBy);
        }

        public void Approve(string approver) => AuditInfo.Approve(approver);
    }
}
