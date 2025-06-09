using MicroNet.Employee.Core.Dtos.External;

namespace MicroNet.Employee.Core.Clients
{
    public interface IAuditLogServiceClient
    {
        Task<AuditLogDto> CreateAuditLogAsync(AuditLogDto logDto);
    }
}
