using MicroNet.Branch.Api.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.Branches.CreateBranch
{
    public class CreateBranchCommand : ICommand<Guid>
    {
        public Guid Id { get; set; }
        public string BranchCode { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public Address Address { get; set; } = default!;
        public string Region { get; set; } = default!;
        public DateTime SetupDate { get; set; }
        public string BranchManagerName { get; set; } = default!;
        public ProductSummary ProductSummary { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}
