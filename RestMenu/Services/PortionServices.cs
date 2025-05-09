using Microsoft.EntityFrameworkCore;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public class PortionServices:IPortionServices
    {
        private  readonly AppDbContext _context;
        public PortionServices(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<PortionResDTO> CreateAsync(PortionResDTO portionDto)
        {
            var portion = new Portion
            {
                Size = portionDto.Size,
                Price = portionDto.Price,
                MenuItemId = portionDto.MenuItemId,


            };
            await _context.AddAsync(portion);
            await _context.SaveChangesAsync();

            return new PortionResDTO
            {
                Id = portion.Id,
                Size = portion.Size,
                Price = portion.Price,
                MenuItemId = portion.MenuItemId
            };
        }

     

        public async Task<IEnumerable<PortionResDTO>> GetAllAsync()
        {
            var portion = await _context.Portions.ToListAsync();

            if (portion == null|| !portion.Any())
            {
                return new List<PortionResDTO>();

            }
             var result= new List<PortionResDTO>();

            foreach (var item in portion)
            {
                var portionDTO = new PortionResDTO
                {
                    Size = item.Size,
                    Price = item.Price,
                    MenuItemId = item.MenuItemId,

                };

                result.Add(portionDTO);
            }
            return result;
        }

        public async Task<PortionResDTO> GetByIdAsync(Guid id)
        {
            var portion= await _context.Portions.FirstOrDefaultAsync(x=> x.Id==id);

            if (portion == null)
            {
                return null;
            }

            var portionDTO = new PortionResDTO
            {
                Id = portion.Id,
                Size = portion.Size,
                Price = portion.Price,
                MenuItemId = portion.MenuItemId,
            };

            return portionDTO;
        }

        public async Task<PortionResDTO> UpdateAsync(Guid id, PortionResDTO portionDto)
        {
            var portion = await _context.Portions.FirstOrDefaultAsync(x => x.Id == id);
            if (portion == null)
            {
                return null;
            }

            portion.Id = portionDto.Id;
            portion.Size = portionDto.Size;
            portionDto.Price = portionDto.Price;
            portion.MenuItemId = portionDto.MenuItemId;
            await _context.SaveChangesAsync();

            var result = new PortionResDTO
            {
                Id = portion.Id,
                Size = portionDto.Size,
                Price = portionDto.Price,
                MenuItemId = portionDto.MenuItemId,
            };
            return result;
        }

        

        public  async Task<bool> DeleteAsync(Guid id)
        {
            var portion = await _context.Portions.FirstOrDefaultAsync(x => x.Id == id);
            if (portion == null)
            {
                return false;
            }

             _context.Remove(portion);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
