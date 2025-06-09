using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetPenaltyChargeByIdQueryHandler : IQueryHandler<GetPenaltyChargeByIdQuery, PenaltyChargeDto>
    {
        private readonly IPenaltyChargeRepository _repository;

        public GetPenaltyChargeByIdQueryHandler(IPenaltyChargeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PenaltyChargeDto> Handle(GetPenaltyChargeByIdQuery request, CancellationToken cancellationToken)
        {
            var pc = await _repository.GetByIdAsync(request.Id);
            if (pc == null) throw new PenaltyChargeIdNotFoundException(request.Id);

            return new PenaltyChargeDto
            {
                Id = pc.Id,
                AccountNumber = pc.AccountNumber,
                Amount = pc.Amount,
                Code = pc.Reason.Code,
                Description = pc.Reason.Description,
                CreatedBy = pc.AuditInfo.CreatedBy!,
                ChargedAt = pc.ChargedAt
            };
        }
    }
}
