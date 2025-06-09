using MicroNet.Product.Core.Clients;
using MicroNet.Product.Core.Dtos.External;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MicroNet.Product.Infrastructure.Clients
{
    public class AuditLogServiceClient : IAuditLogServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<AuditLogServiceClient> _logger;

        public AuditLogServiceClient(IHttpClientFactory httpClientFactory,
            IDistributedCache cache, ILogger<AuditLogServiceClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient("UserService");
            _cache = cache;
            _logger = logger;
        }

        public async Task<AuditLogDto> CreateAuditLogAsync(AuditLogDto logDto)
        {
            const string cacheKey = "audit_log_cache";

            // Optional: Check if already cached
            var cachedLog = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedLog))
            {
                _logger.LogInformation("Loaded audit log from Redis cache.");
                return JsonSerializer.Deserialize<AuditLogDto>(cachedLog)!;
            }

            // Prepare content
            var content = new StringContent(
                JsonSerializer.Serialize(logDto),
                Encoding.UTF8,
                "application/json"
            );

            // Send POST request
            var response = await _httpClient.PostAsync("/api/auditlog/CreateAuditLog", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Audit log POST failed: {StatusCode}", response.StatusCode);
                throw new HttpRequestException("Failed to post audit log");
            }

            // Read response
            var result = await response.Content.ReadFromJsonAsync<AuditLogDto>();

            // Cache the result
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            var serialized = JsonSerializer.Serialize(result);
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return result!;
        }
    }
}
