using MicroNet.Menu.Api.Dtos;

namespace MicroNet.Menu.Api.Services
{
    //Create the interface for caching
    public interface IMenuCacheService
    {
        Task InvalidateMenuCacheAsync();
        Task<IEnumerable<MenuDto>> GetOrSetMenusAsync(Func<Task<IEnumerable<MenuDto>>> factory);
    }
}
