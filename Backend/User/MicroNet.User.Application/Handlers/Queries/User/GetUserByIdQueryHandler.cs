using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Exceptions.User;
using MicroNet.User.Application.Queries.User;
using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.User
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, AllUserDto>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetUserByIdAsync(request.Id);

            if (data == null)
                throw new UserIdNotFoundException(request.Id);

            return new AllUserDto
            {
                Id = data.Id,
                UserName = data.UserName,
                FullName = data.FullName,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email,
                PhysicalAddress = data.PhysicalAddress,
                PostalAddress = data.PostalAddress,
                UserImage = data.UserImage,
                CreateDate = data.CreateDate,
                CreateBy = data.CreateBy,
                //CreatedByName = data.CreatedByName,
                Status = data.Status
            };
        }
    }
}
