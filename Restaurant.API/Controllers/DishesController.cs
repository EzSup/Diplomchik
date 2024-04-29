using Azure;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Categories;
using Restaurant.API.Contracts.Dishes;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Dtos.Dish;
using Restaurant.Core.Models;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishesService _dishesService;

        public DishesController(IDishesService dishesService)
        {
            _dishesService = dishesService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<DishResponse>?>> GetAll()
        {
            var dishes = (await _dishesService.GetAll()).ToList() ?? new List<Dish>();
            var response = dishes.Adapt<List<DishResponse>>();
            for (int i = 0; i<response.Count; i++)
            {
                response[i].DiscountPercents = dishes[i]?.Discount?.PecentsAmount == null ? 0 : dishes[i].Discount.PecentsAmount;
            }
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<DishResponse>>> GetByPage(int page, int pageSize)
        {
            var dishes = (await _dishesService.GetByPage(page, pageSize)).ToList();
            var response = dishes.Adapt<List<DishResponse>>();
            for (int i = 0; i < response.Count; i++)
            {
                response[i].DiscountPercents = dishes[i]?.Discount?.PecentsAmount == null ? 0 : dishes[i].Discount.PecentsAmount;
            }
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<DishResponse>> Get(Guid id)
        {
            //var dish = (await _dishesService.GetAll()).FirstOrDefault(x => x.Id == id);
            var dish = await _dishesService.GetById(id);
            var response = dish.Adapt<DishResponse>();
            response.DiscountPercents = dish?.Discount?.PecentsAmount == null ? 0 : dish.Discount.PecentsAmount;
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<DishDataPageResponse>> GetDataPage(Guid id)
        {
            var dish = await _dishesService.GetDishDataById(id);
            return Ok(dish);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<DishPaginationResponse>>> GetByFilerPage([FromBody] DishPaginationRequest request)
        {
            var dishes = await _dishesService.GetByFilter(request);

            //List<DishPaginationResponse> response = ToPaginationResponse(dishes);

            return Ok(dishes);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<int>> GetPagesCount([FromBody] DishPaginationRequest request)
        {
            var dishes = await _dishesService.PagesCount(request);

            //List<DishPaginationResponse> response = ToPaginationResponse(dishes);

            return Ok(dishes);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Guid>> Post([FromBody] DishCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var dish = request.Adapt<Dish>();
                dish.CategoryId = Guid.Empty == request.CategoryId ? null : request.CategoryId;
                dish.CuisineId = Guid.Empty == request.CuisineId ? null : request.CuisineId;
                return Ok(await _dishesService.Add(dish));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddDiscount(Guid dishId, double PercentsAmount)
        {
            try
            {
                if(await _dishesService.AddDiscount(dishId, PercentsAmount))
                    return NoContent();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{dishId:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> RemoveDiscount(Guid dishId)
        {
            try
            {
                if (await _dishesService.RemoveDiscount(dishId))
                    return NoContent();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(Guid id, [FromBody] DishRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var dish = request.Adapt<Dish>();
                dish.Id = id;
                await _dishesService.Update(dish);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BlogsController>/5
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _dishesService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<int>> Purge([FromBody] IEnumerable<Guid> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _dishesService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
