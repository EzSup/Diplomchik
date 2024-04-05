using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.API.Contracts.Tables;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Models;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITablesService _tablesService;

        public TablesController(ITablesService tablesService)
        {
            _tablesService = tablesService;
        }

        // GET: api/<BlogsController>
        [HttpGet("[action]")]
        public async Task<ActionResult<ICollection<TableResponse>>> GetAll()
        {
            var tables = await _tablesService.GetAll();
            var response = tables.Adapt<List<TableResponse>>();
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ICollection<TableResponse>>> GetByPage(int page, int pageSize)
        {
            var tables = await _tablesService.GetByPage(page, pageSize);
            var response = tables.Adapt<List<TableResponse>>();
            return Ok(response);
        }

        [HttpGet("[action]")]       
        public async Task<ActionResult<ICollection<TableResponse>>> GetByFilter(bool? available, decimal minPrice, decimal maxPrice)
        {
            var tables = await _tablesService.GetByFilter(available, minPrice, maxPrice);
            var response = tables.Adapt<List<TableResponse>>();
            return Ok(response);
        }

        // GET api/<BlogsController>/5
        [HttpGet("[action]/{id:guid}")]
        public async Task<ActionResult<TableResponse>> Get(Guid id)
        {
            var table = await _tablesService.GetById(id);
            var response = table.Adapt<TableResponse>();
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<Guid>> Post([FromBody] TableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var blog = request.Adapt<Table>();
                return Ok(await _tablesService.Add(blog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("[action]/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Put(Guid id, [FromBody] TableRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var blog = request.Adapt<Table>();
                blog.Id = id;
                await _tablesService.Update(blog);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BlogsController>/5
        [HttpDelete("[action]/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _tablesService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<ActionResult<int>> Purge([FromBody] IEnumerable<Guid> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _tablesService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
