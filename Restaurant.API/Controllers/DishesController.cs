using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Services.Interfaces;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : Controller
    {
        private readonly IDishesService _dishesService;

        public DishesController(IDishesService dishesService)
        {
            _dishesService = dishesService;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dish>))]
        public async Task<IActionResult> GetAllDishes()
        {
            var dishes = (await _dishesService.GetAll()).Adapt<List<DishDto>>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dishes);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dish>))]
        public async Task<IActionResult> GetAvailableDishes()
        {
            var dishes = (await _dishesService.GetAvailable()).Adapt<List<DishDto>>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dishes);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dish>))]
        public async Task<IActionResult> GetDishesOnSale()
        {
            var dishes = (await _dishesService.GetDishesOnSale()).Adapt<List<DishDto>>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dishes);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(DishDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int Id)
        {
            var dish = await _dishesService.Get(Id);

            if (dish is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dish.Adapt<DishDto>());
        }

        [HttpGet("[action]{Id}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetResultingPrice(int Id)
        {
            try
            {
                var price = await _dishesService.GetDishResultingPrice(Id);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(price);
            }
            catch (ArgumentNullException ex) {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] DishForCreateDto dishCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if ((await _dishesService.Create(dishCreate)) == 0)
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("[action]")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] DishDto updatedDish)
        {
            if (updatedDish is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!(await _dishesService.Update(updatedDish)))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut("[action]")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SetDiscount(int id, double discount)
        {
            if (id < 1 || discount <= 0 || discount > 100)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _dishesService.AddDiscount(id, discount) < 0)
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("[action]/{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!(await _dishesService.Delete(Id)))
                {
                    ModelState.AddModelError("",
                        "Something went wrong deleting dish. Possibly there are financial operations of this type.");
                    return BadRequest(ModelState);
                }
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}