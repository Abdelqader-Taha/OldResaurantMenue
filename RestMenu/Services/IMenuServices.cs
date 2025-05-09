using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public interface IMenuServices
    {
        Task<MenuResDTO> CreateAsync(MenuResDTO menuDto);

        Task<MenuResDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<MenuResDTO>> GetAllAsync();

        Task<MenuResDTO> UpdateAsync(Guid id, MenuResDTO menuDto);

        Task<bool> DeleteAsync(Guid id);
    }
}
