using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.BranchTerminationRules.CreateBranchTerminationRule
{
    public record CreateBranchTerminationRuleCommand(string Code, string Name,
        string Description, string? Notes, string CreatedBy) : ICommand<Guid>;
}
