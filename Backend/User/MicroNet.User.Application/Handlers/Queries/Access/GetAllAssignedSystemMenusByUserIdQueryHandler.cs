using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Access;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Access
{
    public class GetAllAssignedSystemMenusByUserIdQueryHandler : IQueryHandler<GetAllAssignedSystemMenusByUserIdQuery, AssignedMenusDto1>
    {
        private readonly IUserAccessRepository _repository;

        public GetAllAssignedSystemMenusByUserIdQueryHandler(IUserAccessRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<AssignedMenusDto1> Handle(GetAllAssignedSystemMenusByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAssignedSystemMenusByUserIdAsync(request.UserId)
                ?? throw new InvalidOperationException($"No assigned menus found for user with ID: {request.UserId}");
        }
    }
}
