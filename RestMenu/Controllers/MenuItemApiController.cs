using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestMenu.Models.ResDTO;
using RestMenu.Services;
using System;
using System.Threading.Tasks;

namespace RestMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemApiController : ControllerBase
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemApiController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }


        [HttpPost]
        public async Task<IActionResult> CreateMenuItemAsync([FromBody] MenuItemResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Invalid MenuItem data");
            }

            var menuItem = await _menuItemServices.CreateAsync(req);
            return Ok(menuItem);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemServices.GetAllAsync();

            if (menuItems == null)
            {
                return NotFound("No MenuItems found.");
            }

            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(Guid id)
        {
            var menuItem = await _menuItemServices.GetByIdAsync(id);

            if (menuItem == null)
            {
                return NotFound($"MenuItem with ID {id} not found.");
            }

            return Ok(menuItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItemAsync(Guid id, [FromBody] MenuItemResDTO req)
        {
            if (req == null)
            {
                return BadRequest("MenuItem data is required.");
            }

            var updatedMenuItem = await _menuItemServices.UpdateAsync(id, req);

            if (updatedMenuItem == null)
            {
                return NotFound($"MenuItem with ID {id} not found.");
            }

            return Ok(updatedMenuItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItemAsync(Guid id)
        {
            var isDeleted = await _menuItemServices.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"MenuItem with ID {id} not found.");
            }

            return Ok("MenuItem deleted successfully.");
        }
    }
}
