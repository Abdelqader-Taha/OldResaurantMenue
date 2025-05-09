using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestMenu.Models.ResDTO;
using RestMenu.Services;

namespace RestMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantApiController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;
        public RestaurantApiController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;

        }


        [HttpPost]
        public async Task<IActionResult> CreateResturantAsync([FromBody] RestaurantResDTO req)
        {
            if (req == null)
            {
                return BadRequest("Invalid restaurant data");
            }
            var rest = await _restaurantServices.CreateAsync(req);

            return Ok(rest);
        }

        [HttpGet]
        public async Task<IActionResult> GetALlRestuarantAsync()
        {
            var rest = await _restaurantServices.GetAllAsync();
            if (rest == null)
            {
                return NotFound("Restaurant Not Found ");
            }
            return Ok(rest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestauranById(Guid id)
        {
            var rest = await _restaurantServices.GetByIdAsync(id);

            if (rest == null)
            {
                return NotFound($"Restaurant With Id {id} Not Found ");
            }
            return Ok(rest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurantAsync(Guid id, [FromBody] RestaurantResDTO req)
        {
            if (req ==null)
            {
                return BadRequest("Restuarant Data Is required ");
            }
            var updatedrest= await _restaurantServices.UpdateAsync(id, req);   
            if (updatedrest == null)
            {
                return NotFound($"Restaurant With Id {id} Not Found");
            }
            return Ok(updatedrest);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            var isDeleted = await _restaurantServices.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Restaurant with ID {id} not found.");


            }
            return Ok("Restaurant Deleted successfully.");

        }




    }
}
