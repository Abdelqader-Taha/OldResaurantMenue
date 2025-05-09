using Microsoft.EntityFrameworkCore;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.Services
{
    public class MenuItemServices:IMenuItemServices
    {
        private readonly AppDbContext _context;
        public MenuItemServices(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<MenuItemResDTO> CreateAsync(MenuItemResDTO menuItemDto)
        {
            var menuItem = new MenuItem
            {
                Name = menuItemDto.Name,
                ImagePath = menuItemDto.ImagePath,
                Description = menuItemDto.Description,
                MenuCategoryID = menuItemDto.MenuCategoryID
            };

            await _context.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            return new MenuItemResDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                ImagePath = menuItem.ImagePath,
                Description = menuItem.Description,
                MenuCategoryID = menuItem.MenuCategoryID
            };
        }




        public async Task<IEnumerable<MenuItemResDTO>> GetAllAsync()
        {
            var menuItems = await _context.MenuItems
                .Include(x => x.Portions) 
                .ToListAsync();

            if (menuItems == null || !menuItems.Any())
            {
                return new List<MenuItemResDTO>();
            }

            var result = menuItems.Select(item => new MenuItemResDTO
            {
                Id = item.Id,
                Name = item.Name ?? string.Empty, 
                ImagePath = item.ImagePath ?? string.Empty,
                Description = item.Description ?? string.Empty,
                MenuCategoryID = item.MenuCategoryID,
                Portions = item.Portions?.Select(p => new PortionResDTO
                {
                    Id = p.Id,
                    Size = p.Size ?? string.Empty, // Ensure Size is not null
                    Price = p.Price,
                    MenuItemId = p.MenuItemId
                }).ToList() ?? new List<PortionResDTO>() 
            }).ToList();

            return result;
        }

        public async Task<MenuItemResDTO> GetByIdAsync(Guid id)
        {
            var menuItem = await _context.MenuItems
                .Include(x => x.Portions) 
                .FirstOrDefaultAsync(x => x.Id == id);

            if (menuItem == null)
            {
                return null!;
            }

            var menuItemDTO = new MenuItemResDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name ?? string.Empty,
                ImagePath = menuItem.ImagePath ?? string.Empty,
                Description = menuItem.Description ?? string.Empty,
                MenuCategoryID = menuItem.MenuCategoryID,
                Portions = menuItem.Portions.Select(p => new PortionResDTO
                {
                    Id = p.Id,
                    Size = p.Size,
                    Price = p.Price,
                    MenuItemId = p.MenuItemId
                }).ToList()
            };

            return menuItemDTO;
        }

        public async Task<MenuItemResDTO> UpdateAsync(Guid id, MenuItemResDTO menuItemDto)
        {

            var menuItem = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
            if (menuItem == null)
            {
                return null;
            }

            menuItem.MenuCategoryID = menuItemDto.MenuCategoryID;
            menuItem.Name = menuItemDto.Name;
            menuItem.ImagePath = menuItemDto.ImagePath;
            menuItem.Description = menuItemDto.Description;

            //here i dont need to update portion because  its data become from portion Model

            await _context.SaveChangesAsync();

            var result = new MenuItemResDTO
            {
                Id = menuItem.Id,
                MenuCategoryID = menuItem.MenuCategoryID,
                Name = menuItem.Name,
                ImagePath = menuItem.ImagePath,
                Description = menuItem.Description,

                Portions = menuItem.Portions.Select(p => new PortionResDTO
                {
                    Id = p.Id,
                    Size = p.Size,
                    Price = p.Price,
                    MenuItemId = p.MenuItemId
                }).ToList()
            };

            return result;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var menuItem = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (menuItem == null)
            {
                return false;
            }
            _context.MenuItems.Remove(menuItem);
             await _context.SaveChangesAsync(); 
            return true;
        }
    }
}
