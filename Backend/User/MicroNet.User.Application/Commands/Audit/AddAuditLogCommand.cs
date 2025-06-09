using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.Audit;

namespace MicroNet.User.Application.Commands.Audit
{
    public class AddAuditLogCommand : ICommand<AuditLogDto>
    {
        public Guid Id { get; set; }
        public DateTime AuditDate { get; set; } = DateTime.Now;
        public string UserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Data { get; set; } = default!;
        public string Method { get; set; } = default!;
        public string EntityType { get; set; } = default!;
    }
}
