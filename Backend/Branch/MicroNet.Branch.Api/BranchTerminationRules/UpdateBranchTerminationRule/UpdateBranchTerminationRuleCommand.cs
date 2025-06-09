using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Branch.Api.BranchTerminationRules.UpdateBranchTerminationRule
{
    public record UpdateBranchTerminationRuleCommand(Guid Id, string Name,
        string Description, string? Notes, string UpdatedBy) : ICommand;
}
