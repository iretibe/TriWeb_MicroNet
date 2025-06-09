using MicroNet.Shared.CQRS.Commands;
using MicroNet.System.Core.Dtos;

namespace MicroNet.System.Application.Commands
{
    public class UpdateCompanySetupCommand : ICommand
    {
        public Guid Id { get; }
        public CompanySetupDto Dto { get; }

        public UpdateCompanySetupCommand(Guid id, CompanySetupDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
