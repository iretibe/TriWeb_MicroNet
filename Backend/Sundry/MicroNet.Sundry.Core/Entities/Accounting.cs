using MicroNet.Sundry.Core.Enums;
using MicroNet.Sundry.Core.ValueObjects;

namespace MicroNet.Sundry.Core.Entities
{
    public class Accounting : AggregateRoot
    {
        public string Code { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public AccountCategory Category { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private Accounting() { }

        public Accounting(string code, string name, AccountCategory category, string createdBy)
        {
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            Category = category;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Update(string name, string updatedBy)
        {
            Name = name;
            AuditInfo.SetUpdated(updatedBy);
        }
    }
}
