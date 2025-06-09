using MicroNet.Product.Core.Dtos.External;

namespace MicroNet.Product.Core.Clients
{
    public interface IAuditLogServiceClient
    {
        Task<AuditLogDto> CreateAuditLogAsync(AuditLogDto logDto);
    }
}
