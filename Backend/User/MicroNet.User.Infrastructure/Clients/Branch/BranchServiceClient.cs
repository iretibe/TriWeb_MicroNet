using MicroNet.User.Core.Dto.Branch;
using MicroNet.User.Infrastructure.Clients.Menu;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicroNet.User.Infrastructure.Clients.Branch
{
    public class BranchServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<MenuServiceClient> _logger;

        public BranchServiceClient(IHttpClientFactory httpClientFactory,
            IDistributedCache cache, ILogger<MenuServiceClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient("MenuService");
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync()
        {
            const string cacheKey = "all_branches_cache";
            var cachedBranches = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedBranches))
            {
                _logger.LogInformation("Loaded branches from Redis cache.");
                return JsonSerializer.Deserialize<IEnumerable<BranchDto>>(cachedBranches)!;
            }

            _logger.LogInformation("Fetching branches from BranchService.");
            var response = await _httpClient.GetAsync("/api/Branches/GetAllBranches");

            if (!response.IsSuccessStatusCode)
            {
                //Handle error or log it
                throw new Exception("Branch service call failed");
            }

            var branches = await response.Content.ReadFromJsonAsync<IEnumerable<BranchDto>>();

            //Cache it for 30 minutes
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            var serialized = JsonSerializer.Serialize(branches);
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return branches!;
        }

        public async Task<BranchDto> GetBranchByIdAsync(Guid branchId)
        {
            const string cacheKey = "branch_by_id_cache";
            var cachedBranchById = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedBranchById))
            {
                _logger.LogInformation("Loaded branch by id from Redis cache.");
                return JsonSerializer.Deserialize<BranchDto>(cachedBranchById)!;
            }

            _logger.LogInformation("Fetching branches from BranchService.");
            var response = await _httpClient.GetAsync($"/api/Branches/GetBranchById/{branchId}");

            if (!response.IsSuccessStatusCode)
            {
                //Handle error or log it
                throw new Exception("Branch by Id service call failed");
            }

            var branchById = await response.Content.ReadFromJsonAsync<BranchDto>();

            //Cache it for 30 minutes
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            var serialized = JsonSerializer.Serialize(branchById);
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return branchById!;
        }
    }
}
