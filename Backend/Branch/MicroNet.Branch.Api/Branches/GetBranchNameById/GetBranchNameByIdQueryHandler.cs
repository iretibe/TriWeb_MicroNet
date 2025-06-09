using MicroNet.Branch.Api.Data;
using MicroNet.Shared.CQRS.Queries;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Branch.Api.Branches.GetBranchNameById
{
    public class GetBranchNameByIdQueryHandler : IQueryHandler<GetBranchNameByIdQuery, string>
    {
        private readonly BranchContext _context;

        public GetBranchNameByIdQueryHandler(BranchContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetBranchNameByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Branches
                .Where(b => b.Id == request.BranchId)
                .Select(b => b.BranchName)
                .FirstOrDefaultAsync(cancellationToken);

            return result ?? string.Empty; // or throw if not found
        }
    }
}
