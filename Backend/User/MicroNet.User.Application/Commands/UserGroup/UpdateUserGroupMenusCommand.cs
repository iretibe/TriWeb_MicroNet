using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Commands.UserGroup
{
    public class UpdateUserGroupMenusCommand : ICommand<UpdateUserGroupDto>
    {
        public UpdateUserGroupMenusCommand(UpdateUserGroupDto entity)
        {
            Entity = entity;
        }
        public UpdateUserGroupDto Entity { get; set; }
    }
}
