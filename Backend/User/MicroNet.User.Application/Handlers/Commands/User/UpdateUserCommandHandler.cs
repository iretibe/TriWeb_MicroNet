using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Events.User;
using MicroNet.User.Core.Entities;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Models;
using MicroNet.User.Application.Commands.User;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroNet.command.Application.Handlers.Commands.User
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ILogger<UpdateUserCommandHandler> _loggerService;

        public UpdateUserCommandHandler(IUserRepository repository,
            IMessageBroker messageBroker, IDomainEventLogger logger,
            IConfiguration configuration, IAuditLogRepository auditLogRepository,
            ILogger<UpdateUserCommandHandler> loggerService)
        {
            _repository = repository;
            _messageBroker = messageBroker;
            _logger = logger;
            _configuration = configuration;
            _auditLogRepository = auditLogRepository;
            _loggerService = loggerService;
        }

        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            string strFilePath = _configuration.GetSection("ProfilePath").GetSection("Image").Value!;

            if (string.IsNullOrEmpty(command.UserImage) || command.UserImage == "string" ||
                string.IsNullOrWhiteSpace(command.UserImage))
            {

            }
            else if (!string.IsNullOrEmpty(command.UserImage))
            {
                string startingFilePath = strFilePath;

                string FilePath = HelperFunctions.SaveImages(command.UserImage, startingFilePath, command.Id);

                FileInfo fInfo = new FileInfo(FilePath);

                command.UserImage = fInfo.Name;
            }

            await _repository.UpdateUserAsync(new AspNetUsers
            {
                Id = command.Id,
                UserName = command!.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                FullName = command!.FullName,
                PostalAddress = command!.PostalAddress,
                PhysicalAddress = command!.PhysicalAddress,
                UserImage = command!.UserImage,
                CreateDate = DateTime.UtcNow,
                CreateBy = command.CreateBy,
                Status = command.Status,
                IsSystemAdmin = command.IsSystemAdmin
            });

            //Publish using Message Broker
            await _messageBroker.PublishAsync(new UserUpdatedEvent(command.Id, command.Id, command.FullName, 
                command.PhysicalAddress, command.PostalAddress, command.UserImage, command.CreateDate, command.CreateBy.ToString(), 
                command.Status, command.UserName, command.Email, command.PhoneNumber), "user.updated");

            //Add the event to the database
            await _logger.LogAsync(new DomainEventLog
            {
                EventType = nameof(UserUpdatedEvent),
                Payload = System.Text.Json.JsonSerializer.Serialize(new UserUpdatedEvent(command.Id, command.Id, command.FullName, 
                    command.PhysicalAddress, command.PostalAddress, command.UserImage, command.CreateDate, command.CreateBy.ToString(), 
                    command.Status, command.UserName, command.Email, command.PhoneNumber)),
                AggregateId = Guid.Parse(command.Id),
                AggregateType = "User",
                OccurredAt = DateTime.UtcNow,
                Retries = 0,
                LastAttemptedAt = DateTime.UtcNow
            });

            var auditTrail = AuditLog.AddAuditLog(
                command.Id,
                Newtonsoft.Json.JsonConvert.SerializeObject(command, SerializationHelper.Settings),
                "Updated a User",
                "AspNetUsers"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            _loggerService.LogInformation("User updated successfully with ID: {UserId}", command.Id);
            return new UserDto
            {
                Id = command.Id,
                UserName = command!.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                FullName = command!.PostalAddress,
                PhysicalAddress = command!.PhysicalAddress,
                UserImage = command!.UserImage,
                CreateDate = DateTime.UtcNow,
                CreateBy = command.CreateBy,
                Status = command.Status
                //IsSystemAdmin = command.IsSystemAdmin
            };
        }
    }
}
