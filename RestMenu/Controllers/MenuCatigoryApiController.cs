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
    public class MenuCatigoryApiController : ControllerBase
    {
        private readonly IMenuCatigoryServices _menuCatigoryServices;

        public MenuCatigoryApiController(IMenuCatigoryServices menuCatigoryServices)
        {
            _menuCatigoryServices = menuCatigoryServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuCategoryAsync([FromBody] MenuCatigoryResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Invalid MenuCategory data");
            }

            var menuCategory = await _menuCatigoryServices.CreateAsync(req);
            return Ok(menuCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuCategoriesAsync()
        {
            var menuCategories = await _menuCatigoryServices.GetAllAsync();

            if (menuCategories == null)
            {
                return NotFound("No MenuCategories found.");
            }

            return Ok(menuCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuCategoryById(Guid id)
        {
            var menuCategory = await _menuCatigoryServices.GetByIdAsync(id);

            if (menuCategory == null)
            {
                return NotFound($"MenuCategory with ID {id} not found.");
            }

            return Ok(menuCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuCategoryAsync(Guid id, [FromBody] MenuCatigoryResDTO req)
        {
            if (req == null)
            {
                return BadRequest("MenuCategory data is required.");
            }

            var updatedMenuCategory = await _menuCatigoryServices.UpdateAsync(id, req);

            if (updatedMenuCategory == null)
            {
                return NotFound($"MenuCategory with ID {id} not found.");
            }

            return Ok(updatedMenuCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuCategoryAsync(Guid id)
        {
            var isDeleted = await _menuCatigoryServices.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"MenuCategory with ID {id} not found.");
            }

            return Ok("MenuCategory deleted successfully.");
        }
    }
}
