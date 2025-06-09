using MicroNet.Menu.Api.Dtos;

namespace MicroNet.Menu.Api.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Entities.Menu>> GetAllMenuAsync();
        Task<Entities.Menu> GetMenusByIdAsync(Guid menuId);
        Task<IEnumerable<MenuDto>> GetAllSystemMenusAsync();
        Task<IEnumerable<SubMenuDto>> GetAllSystemSubMenusByIdAsync(Guid menuId);
    }
}
