using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.Menu;

namespace MicroNet.User.Application.Commands.Access
{
    public class AssignMenusToUserCommand : ICommand<AssignedMenusDto1>
    {
        public AssignedMenusDto1 AssignedMenus { get; set; }
        public AssignMenusToUserCommand(AssignedMenusDto1 assignedMenus)
        {
            AssignedMenus = assignedMenus;
        }
    }
}
