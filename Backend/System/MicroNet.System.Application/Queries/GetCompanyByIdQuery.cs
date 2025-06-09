using MicroNet.Shared.CQRS.Queries;
using MicroNet.System.Core.Entities;

namespace MicroNet.System.Application.Queries
{
    public class GetCompanyByIdQuery : IQuery<CompanySetup>
    {
        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
