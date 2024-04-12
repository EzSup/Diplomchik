using Azure;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Categories;
using Restaurant.API.Contracts.Dishes;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Dtos;
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
        public async Task<ActionResult<ICollection<DishResponse>?>> GetAll()
        {
            var dishes = (await _dishesService.GetAll()) ?? new List<Dish>();
            var response = dishes.Adapt<List<DishResponse>>();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<DishResponse>>> GetByPage(int page, int pageSize)
        {
            var dishes = await _dishesService.GetByPage(page, pageSize);
            var response = dishes.Adapt<List<DishResponse>>();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DishResponse>> Get(Guid id)
        {
            //var dish = (await _dishesService.GetAll()).FirstOrDefault(x => x.Id == id);
            var dish = await _dishesService.GetById(id);
            var response = dish.Adapt<DishResponse>();
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ICollection<DishPaginationResponse>>> GetByFilerPage([FromBody] DishPaginationRequest request)
        {
            var dishes = await _dishesService.GetByFilterPage(
                request.pageIndex, request.pageSize,
                request.Name, request.MinWeight, request.MaxWeight,
                request.Ingredients, request.Available,
                request.MinPrice, request.MaxPrice,
                request.Category, request.Cuisine,
                request.MinDiscountsPercents);

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
                return Ok(await _dishesService.Add(dish));
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

        private List<DishPaginationResponse> ToPaginationResponse(ICollection<Dish> dishes)
        {
            List<DishPaginationResponse> response = new();
            foreach (var dish in dishes)
            {
                response.Add(new(dish.Id,
                    dish.Name!,
                    dish.PhotoLinks.First() ?? "",
                    dish.Price,
                    (dish.Price * 0.01m * (decimal)dish.Discount.PecentsAmount)));
            }
            return response;
        }
    }
}
