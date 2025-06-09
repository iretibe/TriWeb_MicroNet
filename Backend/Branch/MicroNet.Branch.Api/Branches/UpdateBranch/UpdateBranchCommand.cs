using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.Branches.UpdateBranch
{
    public class UpdateBranchCommand : ICommand
    {
        public Guid Id { get; set; }
        public string BranchCode { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;
        public string Region { get; set; } = default!;
        public DateTime SetupDate { get; set; }
        public string BranchManagerName { get; set; } = default!;
        public ProductSummaryDto ProductSummary { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}
