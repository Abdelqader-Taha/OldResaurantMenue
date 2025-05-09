using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestMenu.Models.ResDTO;
using RestMenu.Services;

namespace RestMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortionApiController : ControllerBase
    {

        private readonly IPortionServices _portionServices;
        public PortionApiController(IPortionServices portionServices)
        {
            _portionServices = portionServices;

        }

        [HttpPost]
        public async Task<IActionResult> CreatePortionAsync([FromBody] PortionResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Invalid Portion data");
            }
            var portion = await _portionServices.CreateAsync(req);
            return Ok(portion);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortionsAsync()
        {
            var portion = await _portionServices.GetAllAsync();

            if (portion == null)
            {
                return NotFound("The portion Not Found");
            }
            return Ok(portion);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortionById(Guid id)
        {
            var portion = await _portionServices.GetByIdAsync(id);
            if (portion == null)
            {
                return NotFound($"Portion With Id {id} Not Found ");
            }
            return Ok(portion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortionAsync(Guid id, PortionResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Portion Data Is required ");
            }
            var updatedPortion = await _portionServices.UpdateAsync(id, req);
            if (updatedPortion == null)
            {
                return NotFound($"Portion With Id {id} Not Found");
            }
            return Ok(updatedPortion);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortionAsync(Guid id)
        {
            var isDeleted = await _portionServices.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Portion with ID {id} not found.");


            }
            return Ok("Portion Deleted successfully.");
        }

    }
}
