using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Access;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Access
{
    public class GetUserSystemMenusForUpdateQueryHandler : IQueryHandler<GetUserSystemMenusForUpdateQuery, AssignedMenusForUpdateDto>
    {
        private readonly IUserAccessRepository _repository;

        public GetUserSystemMenusForUpdateQueryHandler(IUserAccessRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<AssignedMenusForUpdateDto> Handle(GetUserSystemMenusForUpdateQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetUserSystemMenusForUpdateAsync(request.UserId)
                ?? throw new InvalidOperationException($"No menus found for user with ID: {request.UserId}");
        }
    }
}
