using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.UserGroup
{
    public class GetWorkingDaysByUserGroupIdQueryHandler : IQueryHandler<GetWorkingDaysByUserGroupIdQuery, List<GetWorkingDayDto>>
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public GetWorkingDaysByUserGroupIdQueryHandler(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<List<GetWorkingDayDto>> Handle(GetWorkingDaysByUserGroupIdQuery request, CancellationToken cancellationToken)
        {
            var workingDays = await _userGroupRepository.GetWorkingDaysByUserGroupIdAsync(request.UserGroupId);

            if (workingDays == null || !workingDays.Any())
            {
                return new List<GetWorkingDayDto>();
            }

            return workingDays.Select(day => new GetWorkingDayDto
            {
                DayOfWeek = day.DayOfWeek,
                DayName = day.DayName
            }).ToList();
        }
    }
}
