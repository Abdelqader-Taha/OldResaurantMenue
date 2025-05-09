using Microsoft.EntityFrameworkCore;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public class MenuCatigoryServices:IMenuCatigoryServices
    {
        private  readonly AppDbContext _context;
        public MenuCatigoryServices( AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<MenuCatigoryResDTO> CreateAsync(MenuCatigoryResDTO menuCatigoryDto)
        {
            var menucatigory = new MenuCatigory
            {
                Id = Guid.NewGuid(),
                Name = menuCatigoryDto.Name,
                Description = menuCatigoryDto.Description,
                MenuId = menuCatigoryDto.MenuId,
            };

            await _context.AddAsync(menucatigory);
            await _context.SaveChangesAsync();

            return new MenuCatigoryResDTO
            {
                Id = menucatigory.Id,
                Name = menuCatigoryDto.Name,
                Description = menuCatigoryDto.Description,
                MenuId= menuCatigoryDto.MenuId,
            };
        }



        public async Task<IEnumerable<MenuCatigoryResDTO>> GetAllAsync()
        {
            var menucatigory = await _context.MenuCategories
                .Include(x => x.MenuItems)
                .ToListAsync();

            // Corrected condition: If menucatigory is null or empty, return an empty list
            if (menucatigory == null || !menucatigory.Any())
            {
                return new List<MenuCatigoryResDTO>();
            }

            var result = menucatigory.Select(item => new MenuCatigoryResDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                MenuId = item.MenuId,
                MenuItems = item.MenuItems?.Select(m => new MenuItemResDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImagePath = m.ImagePath,
                    Description = m.Description,
                    MenuCategoryID = m.MenuCategoryID
                }).ToList() ?? new List<MenuItemResDTO>() // Ensure MenuItems is never null

            }).ToList();

            return result;
        }

        public async Task<MenuCatigoryResDTO> GetByIdAsync(Guid id)
        {
            var menuCategory = await _context.MenuCategories
                .Include(x => x.MenuItems) 
                .FirstOrDefaultAsync(x => x.Id == id);

            if (menuCategory == null)
            {
                return null; 
            }

            var result = new MenuCatigoryResDTO
            {
                Id = menuCategory.Id,
                Name = menuCategory.Name,
                Description = menuCategory.Description,
                MenuId = menuCategory.MenuId,
                MenuItems = menuCategory.MenuItems?.Select(m => new MenuItemResDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImagePath = m.ImagePath,
                    Description = m.Description,
                    MenuCategoryID = m.MenuCategoryID
                }).ToList() ?? new List<MenuItemResDTO>() 
            };

            return result;
        }


        public async Task<MenuCatigoryResDTO> UpdateAsync(Guid id, MenuCatigoryResDTO menuCatigoryDto)
        {
            var menuCategory = await _context.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (menuCategory == null)
            {
                return null; 
            }

            menuCategory.Name = menuCatigoryDto.Name;
            menuCategory.Description = menuCatigoryDto.Description;

            await _context.SaveChangesAsync();

            var result = new MenuCatigoryResDTO
            {
                Id = menuCategory.Id,
                Name = menuCategory.Name,
                Description = menuCategory.Description,
                MenuId = menuCategory.MenuId,
                MenuItems = menuCategory.MenuItems?.Select(m => new MenuItemResDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImagePath = m.ImagePath,
                    Description = m.Description,
                    MenuCategoryID = m.MenuCategoryID
                }).ToList() ?? new List<MenuItemResDTO>() 
            };

            return result;
        }


        public  async Task<bool> DeleteAsync(Guid id)
        {
            var menuCategory = await _context.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (menuCategory == null)
            {
                return false;
            }
            _context.MenuCategories.Remove(menuCategory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
