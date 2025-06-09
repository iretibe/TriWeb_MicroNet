using MicroNet.User.Core.Dto.Audit;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Core.Repositories
{
    //Publish it to other services using Grpc
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync();
        Task<AuditLogByIdDto> GetAuditLogByIdAsync(Guid Id);
        Task<AuditLog> AddAuditLogAsync(AuditLog entity);
    }
}
