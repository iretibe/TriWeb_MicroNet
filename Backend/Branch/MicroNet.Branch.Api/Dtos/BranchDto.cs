namespace MicroNet.Branch.Api.Dtos
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public string BranchCode { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;
        public string Region { get; set; } = default!;
        public DateTime SetupDate { get; set; }
        public string BranchManagerName { get; set; } = default!;
        public List<ProductSummaryDto> ProductSummary { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}
