using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public interface IMenuCatigoryServices
    {
        Task<MenuCatigoryResDTO> CreateAsync(MenuCatigoryResDTO menuCatigoryDto);

        Task<MenuCatigoryResDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<MenuCatigoryResDTO>> GetAllAsync();

        Task<MenuCatigoryResDTO> UpdateAsync(Guid id, MenuCatigoryResDTO menuCatigoryDto);

        Task<bool> DeleteAsync(Guid id);
    }
}
