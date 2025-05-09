using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public interface IMenuItemServices
    {
        Task<MenuItemResDTO> CreateAsync(MenuItemResDTO menuItemDto);

        Task<MenuItemResDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<MenuItemResDTO>> GetAllAsync();

        Task<MenuItemResDTO> UpdateAsync(Guid id, MenuItemResDTO menuItemDto);

        Task<bool> DeleteAsync(Guid id);
    }
}
