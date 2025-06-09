using MicroNet.Branch.Api.ValueObjects;

namespace MicroNet.Branch.Api.Entities
{
    public class BranchTerminationRule : AggregateRoot
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string? Notes { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private BranchTerminationRule() { }

        public BranchTerminationRule(string code, string name, string description, string? notes, string createdBy)
        {
            Code = code;
            Name = name;
            Description = description;
            Notes = notes;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Update(Guid id, string name, string description, string? notes, string updatedBy)
        {
            Id = id;
            Name = name;
            Description = description;
            Notes = notes;
            AuditInfo.SetUpdated(updatedBy);
        }
    }
}
