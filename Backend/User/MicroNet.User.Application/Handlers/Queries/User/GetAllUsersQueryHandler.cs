using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.User;
using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.User
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<UserRecordsDto>>
    {
        private readonly IUserRepository _repository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserRecordsDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var audits = await _repository.GetAllUsersAsync();

            var result = audits.Select(d => new UserRecordsDto
            {
                Id = d.Id,
                FullName = d.FullName,
                PhysicalAddress = d.PhysicalAddress,
                PostalAddress = d.PostalAddress,
                UserImage = d.UserImage,
                CreateDate = d.CreateDate,
                CreateBy = d.CreateBy,
                AddedByName = d.AddedByName,
                Status = d.Status,
                StatusName = d.StatusName,
                UserName = d.UserName,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber,
                IsSystemAdmin = d.IsSystemAdmin,
                RoleName = d.RoleName
            });

            return result;
        }
    }
}
