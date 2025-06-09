using MicroNet.Employee.Application.Exceptions;
using MicroNet.Employee.Application.Queries;
using MicroNet.Employee.Core.Dtos;
using MicroNet.Employee.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Employee.Application.Handlers.Queries
{
    public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _repository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var e = await _repository.GetByIdAsync(request.Id);
            if (e == null) throw new EmployeeIdNotFoundException(request.Id);

            return new EmployeeDto
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
            };
        }
    }
}
