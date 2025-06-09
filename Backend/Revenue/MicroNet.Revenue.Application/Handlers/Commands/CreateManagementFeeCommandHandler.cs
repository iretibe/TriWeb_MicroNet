using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    public class CreateManagementFeeCommandHandler : ICommandHandler<CreateManagementFeeCommand, Guid>
    {
        private readonly IManagementFeeRepository _repository;

        public CreateManagementFeeCommandHandler(IManagementFeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateManagementFeeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.managementFeeDto;

            //(string accountNumber, FeeStructure fee, decimal calculatedAmount, string createdBy)
            var feeStructure = new FeeStructure(dto.FeeType, dto.FeeValue, dto.FeeFrequency);

            var managementFee = new ManagementFee(dto.AccountNumber, feeStructure, dto.CalculatedAmount, dto.CreatedBy);

            await _repository.AddAsync(managementFee);

            return managementFee.Id;
        }
    }
}
