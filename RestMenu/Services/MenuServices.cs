using Microsoft.EntityFrameworkCore;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public class MenuServices:IMenuServices
    {
        private readonly AppDbContext _context;
        public MenuServices(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<MenuResDTO> CreateAsync(MenuResDTO menuDto)
        {
            var menu = new Menu
            {
               
                RestaurantId = menuDto.RestaurantId,
                IsActive = menuDto.IsActive
            };
            await  _context.AddAsync(menu);
            await _context.SaveChangesAsync();

            return new MenuResDTO
            {
                Id = menuDto.Id,
                RestaurantId=menuDto.RestaurantId,
                IsActive=menuDto.IsActive
            };
            
        }

      

       public async Task<IEnumerable<MenuResDTO>> GetAllAsync()
        {
            var menus = await _context.Menus
                .Include(m => m.MenuCategories) // Include MenuCategories
                .ToListAsync();

            if (menus == null || !menus.Any())
            {
                return new List<MenuResDTO>();
            }

            var result = menus.Select(menu => new MenuResDTO
            {
                Id = menu.Id,
                RestaurantId = menu.RestaurantId,
                IsActive = menu.IsActive,
                MenuCategories = menu.MenuCategories.Select(c => new MenuCatigoryResDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            }).ToList();

            return result;
        }

public async Task<MenuResDTO> GetByIdAsync(Guid id)
{
    var menu = await _context.Menus
        .Include(m => m.MenuCategories) // Include MenuCategories
        .FirstOrDefaultAsync(m => m.Id == id);

    if (menu == null)
    {
        return null!;
    }

    return new MenuResDTO
    {
        Id = menu.Id,
        RestaurantId = menu.RestaurantId,
        IsActive = menu.IsActive,
        MenuCategories = menu.MenuCategories.Select(c => new MenuCatigoryResDTO
        {
            Id = c.Id,
            Name = c.Name
        }).ToList()
    };
}


        public async Task<MenuResDTO> UpdateAsync(Guid id, MenuResDTO menuDto)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == id);
            if (menu == null)
            {
                return null;
            }

            menu.Id = menuDto.Id;
            menu.RestaurantId = menuDto.RestaurantId;
            menu.IsActive = menuDto.IsActive;

            await _context.SaveChangesAsync();

            var result = new MenuResDTO
            {
                Id = menu.Id,
                RestaurantId = menu.RestaurantId,
                IsActive = menu.IsActive
            };
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == id);
            if (menu == null)
            {
                return false;
            }

            _context.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
