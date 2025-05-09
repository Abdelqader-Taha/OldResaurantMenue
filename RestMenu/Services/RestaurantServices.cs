using Microsoft.EntityFrameworkCore;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly AppDbContext _context;
        public RestaurantServices(AppDbContext context)
        {
            _context = context;

        }


        public async Task<RestaurantResDTO> CreateAsync(RestaurantResDTO restaurantDto)
        {
            var rest = new Restaurant
            {
                Name = restaurantDto.Name,
                PhoneNumber = restaurantDto.PhoneNumber,
                Address = restaurantDto.Address,
                Url = restaurantDto.Url,
                UserId=restaurantDto.UserId

            };

            await _context.AddAsync(rest);
            await _context.SaveChangesAsync();

            return new RestaurantResDTO
            {
                Id = rest.Id,
                Name = restaurantDto.Name,
                PhoneNumber = restaurantDto.PhoneNumber,
                Address = restaurantDto.Address,
                Url = restaurantDto.Url,
                UserId = restaurantDto.UserId

            };

        }

        public async Task<IEnumerable<RestaurantResDTO>> GetAllAsync()
        {

            var rest = await _context.Restaurants.ToListAsync();

            if (rest == null|| !rest.Any())
            {
               return new List<RestaurantResDTO>();

            }

            var result= new List<RestaurantResDTO>();

            foreach ( var restaurant in rest)
            {
                var restDTO = new RestaurantResDTO
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    PhoneNumber = restaurant.PhoneNumber,
                    Address = restaurant.Address,
                    Url = restaurant.Url,
                  
                };
                result.Add(restDTO);

            }
            return result;
                
        }

        public async Task<RestaurantResDTO> GetByIdAsync(Guid id)
        {
            var rest = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
               
            if (rest == null)
            {
                return null;
            }

           
            var restDTO = new RestaurantResDTO
            {
                Id= rest.Id,
                Name = rest.Name,
                PhoneNumber = rest.PhoneNumber,
                Address = rest.Address,
                Url = rest.Url,
            };

            return restDTO;

        }


        public async Task<RestaurantResDTO> UpdateAsync(Guid id, RestaurantResDTO restaurantDto)
        {

            var rest = await _context.Restaurants.FirstOrDefaultAsync(x=> x.Id==id);
            if (rest == null)
            {
                return null;
            }

            //check the new user exist before update
            var userexist = await _context.Users.AnyAsync(x=> x.Id == restaurantDto.UserId);
            if (!userexist)
            {
                throw new InvalidOperationException("The new user Id dose not exist ");

            }


            rest.Name = restaurantDto.Name;
            rest.PhoneNumber = restaurantDto.PhoneNumber;
            rest.Address = restaurantDto.Address;
            rest.Url = restaurantDto.Url;
            rest.UserId = restaurantDto.UserId;
            await _context.SaveChangesAsync();

            var result = new RestaurantResDTO
            {
                Id = rest.Id,
                Name = rest.Name,
                PhoneNumber = rest.PhoneNumber,
                Address = rest.Address,
                Url = rest.Url,
                UserId = rest.UserId,
            };
            return result;


        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rest = await _context.Restaurants.FindAsync(id);


            if ( rest == null)
            {
                return false;
            }
            _context.Restaurants.Remove(rest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
