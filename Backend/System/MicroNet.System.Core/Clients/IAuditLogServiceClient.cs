using MicroNet.System.Core.Dtos.External;

namespace MicroNet.System.Core.Clients
{
    public interface IAuditLogServiceClient
    {
        Task<AuditLogDto> CreateAuditLogAsync(AuditLogDto logDto);
    }
}
