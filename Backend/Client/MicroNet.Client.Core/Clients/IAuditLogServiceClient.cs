using MicroNet.Client.Core.Dtos.External;

namespace MicroNet.Client.Core.Clients
{
    public interface IAuditLogServiceClient
    {
        Task<AuditLogDto> CreateAuditLogAsync(AuditLogDto logDto);
    }
}
