using MicroNet.Client.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Client.Application.Queries
{
    public record GetAllClientsQuery() : IQuery<List<ClientDto>>;
}
