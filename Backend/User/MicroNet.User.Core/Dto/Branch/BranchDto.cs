namespace MicroNet.User.Core.Dto.Branch
{
    public class BranchDto
    {
        public string BranchCode { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string Region { get; set; } = default!;
        public DateTime SetupDate { get; set; }
        public string BranchManagerName { get; set; } = default!;
    }
}
