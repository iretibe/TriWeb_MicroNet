namespace MicroNet.Branch.Api.Dtos
{
    public class BranchTerminationRuleDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? Note { get; set; }
    }
}
