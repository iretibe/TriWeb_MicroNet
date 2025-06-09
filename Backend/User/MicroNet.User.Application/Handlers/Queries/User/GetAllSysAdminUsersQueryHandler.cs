using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.User;
using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.User
{
    public class GetAllSysAdminUsersQueryHandler : IQueryHandler<GetAllSysAdminUsersQuery, IEnumerable<SysAdminRecordsDto>>
    {
        private readonly IUserRepository _repository;

        public GetAllSysAdminUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SysAdminRecordsDto>> Handle(GetAllSysAdminUsersQuery request, CancellationToken cancellationToken)
        {
            var audits = await _repository.GetAllSysAdminUsersAsync();

            var result = audits.Select(d => new SysAdminRecordsDto
            {
                Id = d.Id,
                FullName = d.FullName,
                Status = d.Status,
                StatusName = d.StatusName,
                UserName = d.UserName,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber,
                UserGroupId = d.UserGroupId,
                UserGroupName = d.UserGroupName,
                IsSystemAdmin = d.IsSystemAdmin,
                RoleName = d.RoleName
            });

            return result;
        }
    }
}
