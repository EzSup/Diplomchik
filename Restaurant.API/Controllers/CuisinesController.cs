using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.API.Contracts.Cuisines;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Core.Models;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CuisinesController : ControllerBase
    {
        private readonly ICuisinesService _cuisinesService;

        public CuisinesController(ICuisinesService cuisinesService)
        {
            _cuisinesService = cuisinesService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<CuisineResponse>>> GetAll()
        {
            var cuisines = (await _cuisinesService.GetAll()).ToList();
            var response = cuisines.Adapt<List<CuisineResponse>>();
            for (int i = 0; i < response.Count; i++)
            {
                response[i].DiscountPercents = cuisines[i]?.Discount?.PecentsAmount == null ? 0 : cuisines[i].Discount.PecentsAmount;
            }
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<CuisineResponse>>> GetByPage(int page, int pageSize)
        {
            var cuisines = (await _cuisinesService.GetByPage(page, pageSize)).ToList();
            var response = cuisines.Adapt<List<CuisineResponse>>();
            for (int i = 0; i < response.Count; i++)
            {
                response[i].DiscountPercents = cuisines[i]?.Discount?.PecentsAmount == null ? 0 : cuisines[i].Discount.PecentsAmount;
            }
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<CuisineResponse>> Get(Guid id)
        {
            var cuisine = await _cuisinesService.GetById(id);
            var response = cuisine.Adapt<CuisineResponse>();
            response.DiscountPercents = cuisine?.Discount?.PecentsAmount == null ? 0 : cuisine.Discount.PecentsAmount;
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Guid>> Post([FromBody]CuisineCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var cuisine = new Cuisine{
                    Name = request.Name};
                return Ok(await _cuisinesService.Add(cuisine));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CuisineUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var cuisine = request.Adapt<Cuisine>();
                cuisine.Id = id;
                await _cuisinesService.Update(cuisine);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddDiscount(Guid cuisineId, double PercentsAmount)
        {
            try
            {
                if(await _cuisinesService.AddDiscount(cuisineId, PercentsAmount))
                    return NoContent();

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cuisineId:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> RemoveDiscount(Guid cuisineId)
        {
            try
            {
                if(await _cuisinesService.RemoveDiscount(cuisineId))
                    return NoContent();
                return NotFound();
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
                await _cuisinesService.Delete(id);
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
                return Ok(await _cuisinesService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
