using MicroNet.Account.Core.ValueObjects;

namespace MicroNet.Account.Core.Entities
{
    public class AccountTermination : AggregateRoot
    {
        public AccountDetails TerminatedAccount { get; private set; }
        public string Reason { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public AccountTermination() { }

        public AccountTermination(AccountDetails account, string reason, string createdBy)
        {
            Id = Guid.NewGuid();
            TerminatedAccount = account;
            Reason = reason;
            AuditInfo = new AuditInfo(createdBy);
        }

        public void Approve(string approver) => AuditInfo.Approve(approver);
    }
}
