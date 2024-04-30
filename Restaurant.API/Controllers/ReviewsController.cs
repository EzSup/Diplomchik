using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Tables;
using Restaurant.Application.Interfaces.Services;
using Mapster;
using Restaurant.Core.Dtos.Reviews;
using Restaurant.API.Contracts.Reviews;
using Restaurant.Core.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;
        private readonly ICustomersService _customersService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewsController(IReviewsService reviewsService, ICustomersService customersService, IHttpContextAccessor httpContextAccessor)
        {
            _reviewsService = reviewsService;
            _httpContextAccessor = httpContextAccessor;
            _customersService = customersService;
        }

        // GET: api/<BlogsController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<ReviewResponse>>> GetAll()
        {
            var reviews = await _reviewsService.GetAll();
            var response = reviews.Adapt<List<ReviewResponse>>();
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<ReviewResponse>>> GetByPage(int page, int pageSize)
        {
            var reviews = await _reviewsService.GetByPage(page, pageSize);
            var response = reviews.Adapt<List<ReviewResponse>>();
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<ReviewOfDishResponse>>> GetReviewsOfDish(Guid dishId, int pageIndex, int pageSize)
        {
            var reviews = await _reviewsService.GetReviewsOfDish(dishId, pageIndex, pageSize);
            return Ok(reviews);
        }

        // GET api/<BlogsController>/5
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewResponse>> Get(Guid id)
        {
            var table = await _reviewsService.GetById(id);
            var response = table.Adapt<ReviewResponse>();
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<Guid>> Post([FromBody] ReviewCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var customer = await _customersService.GetByUser(userId);
                var review = request.Adapt<Review>();
                review.AuthorId = customer.Id;
                return Ok(await _reviewsService.Add(review));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ReviewUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var review = request.Adapt<Review>();
                review.Id = id;
                await _reviewsService.Update(review);
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
                await _reviewsService.Delete(id);
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
                return Ok(await _reviewsService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
