using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Commands.User;
using MicroNet.User.Application.Events.User;
using MicroNet.User.Application.Exceptions.User;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Helper;
using MicroNet.User.Core.Logging;
using MicroNet.User.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace MicroNet.User.Application.Handlers.Commands.User
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        //Would be moved to the Email/Notification service later
        private readonly IEmailRepository _emailRepository;
        private readonly IConfiguration _configuration;

        public CreateUserCommandHandler(IUserRepository repository, IMessageBroker messageBroker, 
            IDomainEventLogger logger, IAuditLogRepository auditLogRepository, 
            UserManager<ApplicationUser> userManager, IEmailRepository emailRepository,
            IConfiguration configuration)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
            _auditLogRepository = auditLogRepository;
            _userManager = userManager;
            _emailRepository = emailRepository;
            _configuration = configuration;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string strFrom, strServer, strUser, strPassword;
            int iPort;
            bool bSsl, bStartTls;
            EmailHandler(out strFrom, out strServer, out iPort, out strUser, out strPassword, out bSsl, out bStartTls);

            var vEmail = await _userManager.FindByEmailAsync(request.Email);
            if (vEmail is not null)
            {
                throw new EmailIsInUseException(request.Email);
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                PostalAddress = request.PostalAddress,
                PhysicalAddress = request.PhysicalAddress,
                UserImage = request.UserImage,
                CreateDate = DateTime.UtcNow,
                CreateBy = request.CreateBy,
                Status = false,
                //IsSystemAdmin = request.IsSystemAdmin
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new UserCreationException(result.Errors.ToString()!);
            }

            string strSubject = string.Empty;
            string strBody = string.Empty;

            _emailRepository.EmailMethodToUsers(strFrom, strSubject, strBody, strServer, iPort, strUser,
                strPassword, bSsl, bStartTls, request.FullName, request.UserName, request.Password, request.Email);

            var sysAdmins = await _repository.GetAllSysAdministrators();
            foreach (var sysAdmin in sysAdmins)
            {
                _emailRepository.EmailMethodToSysAdmins(strFrom, strSubject, strBody, strServer, iPort, strUser,
                    strPassword, bSsl, bStartTls, sysAdmin.FullName, request.FullName, request.UserName, sysAdmin.Email);
            }

            var auditTrail = AuditLog.AddAuditLog(
                user.Id,
                Newtonsoft.Json.JsonConvert.SerializeObject(result, SerializationHelper.Settings),
                "Created a User",
                "AspNetUsers"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new UserCreatedEvent(user.Id, user.FullName, user.PhysicalAddress,
                user.PostalAddress, user.UserImage, user.CreateDate, user.CreateBy.ToString(), user.Status, user.Code, user.IsSystemAdmin,
                user.IsFirstTimeLogin, user.UserName, user.Email, user.PasswordHash!, user.PhoneNumber), "user.added");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(UserCreatedEvent),

                Payload = System.Text.Json.JsonSerializer.Serialize(new UserCreatedEvent(user.Id, user.FullName, user.PhysicalAddress,
                    user.PostalAddress, user.UserImage, user.CreateDate, user.CreateBy.ToString(), user.Status, user.Code, user.IsSystemAdmin,
                    user.IsFirstTimeLogin, user.UserName, user.Email, user.PasswordHash!, user.PhoneNumber)),

                AggregateId = Guid.Parse(user.Id),
                AggregateType = "AspNetUsers",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            return new UserDto
            {
                UserName = request!.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request!.FullName,
                PostalAddress = request!.PostalAddress,
                PhysicalAddress = request!.PhysicalAddress,
                UserImage = request!.UserImage,
                CreateDate = DateTime.UtcNow,
                CreateBy = request.CreateBy,
                Status = false,
                //IsSystemAdmin = request.IsSystemAdmin
                //Token = await _tokenRepository.CreateTokenAsync(user)
            };
        }

        //Would be moved to the Email/Notification service later
        private void EmailHandler(out string strFrom, out string strServer, out int iPort, out string strUser, out string strPassword, out bool bSsl, out bool bStartTls)
        {
            strFrom = _configuration.GetSection("EmailConfiguration").GetSection("From").Value!;
            strServer = _configuration.GetSection("EmailConfiguration").GetSection("Host").Value!;
            iPort = Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("Port").Value);
            strUser = _configuration.GetSection("EmailConfiguration").GetSection("Username").Value!;
            strPassword = _configuration.GetSection("EmailConfiguration").GetSection("Password").Value!;
            bSsl = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("UseSSL").Value);
            bStartTls = Convert.ToBoolean(_configuration.GetSection("EmailConfiguration").GetSection("UseStartTls").Value);
        }
    }
}
