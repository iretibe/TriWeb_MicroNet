using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.Menu;

namespace MicroNet.User.Application.Commands.Access
{
    public class UpdateAssignMenusToUserCommand : ICommand<bool>
    {
        public UpdateAssignMenusToUserCommand(AssignedUserMenusForUpdateDto entity)
        {
            Entity = entity;
        }

        public AssignedUserMenusForUpdateDto Entity { get; set; }
    }
}
