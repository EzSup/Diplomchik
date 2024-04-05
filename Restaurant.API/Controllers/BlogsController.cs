using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsService _blogsService;

        public BlogsController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        // GET: api/<BlogsController>
        [HttpGet("[action]")]
        public async Task<ActionResult<ICollection<BlogResponse>>> GetAll()
        {
            var blogs =  await _blogsService.GetAll();
            var response = blogs.Adapt<List<BlogResponse>>();
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ICollection<BlogResponse>>> GetByPage(int page, int pageSize)
        {
            var blogs = await _blogsService.GetByPage(page, pageSize);
            var response = blogs.Adapt<List<BlogResponse>>();
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ICollection<BlogResponse>>> GetByFilter(DateTime? createdAfter, string? authorName, string? titleContains)
        {
            if(createdAfter == null)
                createdAfter = DateTime.MinValue;
            var blogs = await _blogsService.GetByFilter((DateTime)createdAfter, authorName, titleContains);
            var response = blogs.Adapt<List<BlogResponse>>();
            return Ok(response);
        }

        // GET api/<BlogsController>/5
        [HttpGet("[action]/{id:guid}")]
        public async Task<ActionResult<BlogResponse>> Get(Guid id)
        {
            var blog = await _blogsService.GetById(id);
            var response = blog.Adapt<BlogResponse>();
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost("[action]")]
        //[Authorize(Policy ="AdminPolicy")]
        [Authorize]
        public async Task<ActionResult<Guid>> Post([FromBody] BlogRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var blog = request.Adapt<Blog>();
                return Ok(await _blogsService.Add(blog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("[action]/{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Put(Guid id, [FromBody] BlogRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var blog = request.Adapt<Blog>();
                blog.Id = id;
                await _blogsService.Update(blog);
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
                await _blogsService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<ActionResult<int>> Purge([FromBody]IEnumerable<Guid> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {                
                return Ok(await _blogsService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
