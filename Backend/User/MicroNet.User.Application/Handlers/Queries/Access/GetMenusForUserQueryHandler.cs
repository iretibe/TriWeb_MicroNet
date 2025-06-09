using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Access;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Access
{
    public class GetMenusForUserQueryHandler : IQueryHandler<GetMenusForUserQuery, List<MenuEntityDto>>
    {
        private readonly IUserAccessRepository _repository;

        public GetMenusForUserQueryHandler(IUserAccessRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<MenuEntityDto>> Handle(GetMenusForUserQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMenusForUser(request.UserId) 
                ?? throw new InvalidOperationException($"No menus found for user with ID: {request.UserId}");
        }
    }
}
