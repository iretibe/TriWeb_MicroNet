using MicroNet.Shared.CQRS.Queries;
using MicroNet.System.Core.Entities;

namespace MicroNet.System.Application.Queries
{
    public class GetAllCompaniesQuery : IQuery<CompanySetup[]>
    {
    }
}
