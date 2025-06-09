using MicroNet.User.Core.Dto.Menu;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicroNet.User.Infrastructure.Clients.Menu
{
    public class MenuServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<MenuServiceClient> _logger;

        public MenuServiceClient(IHttpClientFactory httpClientFactory,
            IDistributedCache cache, ILogger<MenuServiceClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient("MenuService");
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<MenuDto>> GetAllMenusAsync()
        {
            const string cacheKey = "all_menus_cache";
            var cachedMenus = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedMenus))
            {
                _logger.LogInformation("Loaded menus from Redis cache.");
                return JsonSerializer.Deserialize<IEnumerable<MenuDto>>(cachedMenus)!;
            }

            _logger.LogInformation("Fetching menus from MenuService.");
            var response = await _httpClient.GetAsync("/api/menus/GetAllMenus");

            if (!response.IsSuccessStatusCode)
            {
                //Handle error or log it
                throw new Exception("Menu service call failed");
            }

            var menus = await response.Content.ReadFromJsonAsync<IEnumerable<MenuDto>>();

            //Cache it for 30 minutes
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            var serialized = JsonSerializer.Serialize(menus);
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return menus!;
        }

        public async Task<IEnumerable<MenuEntityDto>> GetAllMenuEntitiesAsync()
        {
            const string cacheKey = "all_menus_cache";
            var cachedMenus = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedMenus))
            {
                _logger.LogInformation("Loaded menus from Redis cache.");
                return JsonSerializer.Deserialize<IEnumerable<MenuEntityDto>>(cachedMenus)!;
            }

            _logger.LogInformation("Fetching menus from MenuService.");
            var response = await _httpClient.GetAsync("/api/menus/GetAllMenus");

            if (!response.IsSuccessStatusCode)
            {
                //Handle error or log it
                throw new Exception("Menu service call failed");
            }

            var menus = await response.Content.ReadFromJsonAsync<IEnumerable<MenuEntityDto>>();

            //Cache it for 30 minutes
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            var serialized = JsonSerializer.Serialize(menus);
            await _cache.SetStringAsync(cacheKey, serialized, cacheOptions);

            return menus!;
        }
    }
}
