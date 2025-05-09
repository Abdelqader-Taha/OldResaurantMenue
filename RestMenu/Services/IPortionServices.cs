using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public interface IPortionServices
    {
        Task<PortionResDTO> CreateAsync(PortionResDTO portionDto);

        Task<PortionResDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<PortionResDTO>> GetAllAsync();

        Task<PortionResDTO> UpdateAsync(Guid id, PortionResDTO portionDto);

        Task<bool> DeleteAsync(Guid id);

    }
}
