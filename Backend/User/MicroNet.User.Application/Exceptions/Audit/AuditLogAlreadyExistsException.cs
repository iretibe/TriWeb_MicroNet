namespace MicroNet.User.Application.Exceptions.Audit
{
    public class AuditLogAlreadyExistsException : AppException
    {
        public AuditLogAlreadyExistsException(Guid id) : base($"Audit with ID: '{id}' already exists!")
        {
        }
    }
}
