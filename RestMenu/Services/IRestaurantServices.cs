using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public interface IRestaurantServices
    {
        Task<RestaurantResDTO> CreateAsync(RestaurantResDTO restaurantDto);

        Task<RestaurantResDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<RestaurantResDTO>> GetAllAsync();

        Task<RestaurantResDTO> UpdateAsync(Guid id, RestaurantResDTO restaurantDto);

        Task<bool> DeleteAsync(Guid id);
    }
}
