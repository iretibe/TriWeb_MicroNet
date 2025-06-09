using MicroNet.Menu.Api.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using RedLockNet.SERedis;
using System.Text.Json;

namespace MicroNet.Menu.Api.Services
{
    //Implement the Redis-based cache service with distributed locking
    public class MenuCacheService : IMenuCacheService
    {
        private const string MenuCacheKey = "all_menus_cache_v1";
        private readonly IDistributedCache _cache;
        private readonly ILogger<MenuCacheService> _logger;
        private readonly RedLockFactory _redLockFactory;

        public MenuCacheService(IDistributedCache cache, ILogger<MenuCacheService> logger, RedLockFactory redLockFactory)
        {
            _cache = cache;
            _logger = logger;
            _redLockFactory = redLockFactory;
        }

        public async Task<IEnumerable<MenuDto>> GetOrSetMenusAsync(Func<Task<IEnumerable<MenuDto>>> factory)
        {
            var cached = await _cache.GetStringAsync(MenuCacheKey);
            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<IEnumerable<MenuDto>>(cached)!;

            //Acquire a distributed lock to avoid cache stampede
            var lockKey = MenuCacheKey + ":lock";
            using var redLock = await _redLockFactory.CreateLockAsync(lockKey, TimeSpan.FromSeconds(10));

            if (!redLock.IsAcquired)
            {
                _logger.LogWarning("Could not acquire lock for caching. Returning empty list.");
                return Enumerable.Empty<MenuDto>();
            }

            //Double-check after acquiring the lock
            cached = await _cache.GetStringAsync(MenuCacheKey);
            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<IEnumerable<MenuDto>>(cached)!;

            var menus = await factory();
            var serialized = JsonSerializer.Serialize(menus);
            await _cache.SetStringAsync(MenuCacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return menus;
        }

        public async Task InvalidateMenuCacheAsync()
        {
            await _cache.RemoveAsync(MenuCacheKey);
        }
    }
}
