using MicroNet.Client.Application.Queries;
using MicroNet.Client.Core.Dtos;
using MicroNet.Client.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Client.Application.Handlers.Queries
{
    public class GetAllClientsQueryHandler : IQueryHandler<GetAllClientsQuery, List<ClientDto>>
    {
        private readonly IClientRepository _repository;

        public GetAllClientsQueryHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repository.GetAllAsync();

            return clients.Select(client => new ClientDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                DateOfBirth = client.DateOfBirth,
                Address = new AddressDto
                {
                    Street = client.Address.Street,
                    City = client.Address.City,
                    State = client.Address.State,
                    ZipCode = client.Address.ZipCode
                },
                KYC = new KYCInformationDto
                {
                    DocumentType = client.KYC.DocumentType,
                    DocumentNumber = client.KYC.DocumentNumber,
                    ExpiryDate = client.KYC.ExpiryDate
                }
            }).ToList();
        }
    }
}
