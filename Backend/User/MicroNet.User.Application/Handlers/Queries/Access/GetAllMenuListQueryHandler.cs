using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Access;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Access
{
    public class GetAllMenuListQueryHandler : IQueryHandler<GetAllMenuListQuery, IEnumerable<MenuEntityDto>>
    {
        private readonly IUserAccessRepository _repository;

        public GetAllMenuListQueryHandler(IUserAccessRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<MenuEntityDto>> Handle(GetAllMenuListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllMenuListAsync().ConfigureAwait(false);
        }
    }
}
