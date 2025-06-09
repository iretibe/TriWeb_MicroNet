namespace MicroNet.User.Application.Exceptions.Audit
{
    public class AuditLogNotFoundException : AppException
    {
        public AuditLogNotFoundException(Guid id) : base($"Audit with ID: '{id}' is not found!")
        {
        }
    }
}
