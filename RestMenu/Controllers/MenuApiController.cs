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
    public class MenuApiController : ControllerBase
    {
        private readonly IMenuServices _menuServices;

        public MenuApiController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuAsync([FromBody] MenuResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Invalid Menu data");
            }

            var menu = await _menuServices.CreateAsync(req);
            return Ok(menu);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenusAsync()
        {
            var menus = await _menuServices.GetAllAsync();

            if (menus == null)
            {
                return NotFound("No menus found.");
            }

            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(Guid id)
        {
            var menu = await _menuServices.GetByIdAsync(id);

            if (menu == null)
            {
                return NotFound($"Menu with ID {id} not found.");
            }

            return Ok(menu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuAsync(Guid id, [FromBody] MenuResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Menu data is required.");
            }

            var updatedMenu = await _menuServices.UpdateAsync(id, req);

            if (updatedMenu == null)
            {
                return NotFound($"Menu with ID {id} not found.");
            }

            return Ok(updatedMenu);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuAsync(Guid id)
        {
            var isDeleted = await _menuServices.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Menu with ID {id} not found.");
            }

            return Ok("Menu deleted successfully.");
        }
    }
}
