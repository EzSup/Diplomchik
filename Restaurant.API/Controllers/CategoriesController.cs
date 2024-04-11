using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.API.Contracts.Cuisines;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Core.Models;
using Restaurant.API.Contracts.Categories;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryResponse>>> GetAll()
        {
            var categories = await _categoriesService.GetAll();
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryResponse>>> GetByPage(int page, int pageSize)
        {
            var categories = await _categoriesService.GetByPage(page, pageSize);
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryResponse>> Get(Guid id)
        {
            var categories = await _categoriesService.GetById(id);
            var response = categories.Adapt<CategoryResponse>();
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Guid>> Post([FromBody] CategoryCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var category = new Category
                {
                    Name = request.Name
                };
                return Ok(await _categoriesService.Add(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var category = request.Adapt<Category>();
                category.Id = id;
                await _categoriesService.Update(category);
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
                await _categoriesService.Delete(id);
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
                return Ok(await _categoriesService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
