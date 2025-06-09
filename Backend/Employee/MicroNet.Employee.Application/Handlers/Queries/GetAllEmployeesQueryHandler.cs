using MicroNet.Employee.Application.Queries;
using MicroNet.Employee.Core.Dtos;
using MicroNet.Employee.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Employee.Application.Handlers.Queries
{
    public class GetAllEmployeesQueryHandler : IQueryHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                EmployeeNumber = e.EmployeeNumber,
                FirstName = e.FirstName,
                Surname = e.Surname,
                OtherName = e.OtherName,
                DateOfBirth = e.DateOfBirth,
                Age = e.Age,
                PhoneNumber = e.PhoneNumber,
                PhysicalAddress = e.PhysicalAddress,
                PostalAddress = e.PostalAddress,
                EmailAddress = e.EmailAddress,
                SocialMediaAccount = e.SocialMediaAccount,
                IsSystemUser = e.IsSystemUser,
                EmployeeType = e.EmployeeType,
                BranchId = e.BranchId,
                DeviceId = e.DeviceId,
                ZoneId = e.ZoneId,
                DateEmployed = e.DateEmployed,
                Picture = e.Picture,
                NationalId = e.NationalId,
                TransactionLimit = e.TransactionLimit,
                ContactPerson = new ContactPersonDto
                {
                    Name = e.ContactPerson.Name,
                    GhanaCard = e.ContactPerson.GhanaCard,
                    PrimaryPhone = e.ContactPerson.PrimaryPhone,
                    AlternatePhone = e.ContactPerson.AlternatePhone,
                    PrimaryEmail = e.ContactPerson.PrimaryEmail,
                    AlternateEmail = e.ContactPerson.AlternateEmail,
                    PhysicalAddress = e.ContactPerson.PhysicalAddress,
                    SocialMedia = e.ContactPerson.SocialMedia
                }
            }).ToList();
        }
    }
}
