namespace MicroNet.User.Core.Dto.Audit
{
    public class AuditLogByIdDto
    {
        public Guid Id { get; set; }
        public DateTime AuditDate { get; set; }
        public string UserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Data { get; set; } = default!;
        public string Method { get; set; } = default!;
        public string EntityType { get; set; } = default!;
    }
}
