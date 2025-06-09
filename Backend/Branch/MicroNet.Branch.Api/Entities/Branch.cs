using MicroNet.Branch.Api.ValueObjects;

namespace MicroNet.Branch.Api.Entities
{
    public class Branch : AggregateRoot
    {
        public string BranchCode { get; private set; }
        public string BranchName { get; private set; }
        public Address PhysicalAddress { get; private set; }
        public string Region { get; private set; }
        public DateTime SetupDate { get; private set; }
        public string ManagerName { get; private set; }
        public List<ProductSummary> ProductSummaries { get; private set; } = new();
        public AuditInfo AuditInfo { get; private set; }

        private Branch() { }

        public Branch(string branchCode, string branchName, Address physicalAddress, string region, DateTime setupDate, string managerName, string createdBy)
        {
            BranchCode = branchCode;
            BranchName = branchName;
            PhysicalAddress = physicalAddress;
            Region = region;
            SetupDate = setupDate;
            ManagerName = managerName;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void UpdateManager(Guid id, string branchName, Address physicalAddress, 
            string region, DateTime setupDate, string managerName, string updatedBy)
        {
            Id = id;
            BranchName = branchName;
            PhysicalAddress = physicalAddress;
            Region = region;
            SetupDate = setupDate;
            ManagerName = managerName;
            AuditInfo.SetUpdated(updatedBy);
        }
    }
}
