using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Models;
using Restaurant.API.Contracts.Deliveries;
using Restaurant.Core.Dtos.Delivery;
using Mapster;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveriesService _deliveriesService;

        public DeliveriesController(IDeliveriesService deliveriesService)
        {
            _deliveriesService = deliveriesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<DeliveryResponse>>> GetAll()
        {
            var blogs = await _deliveriesService.GetAll();
            var response = blogs.Adapt<List<DeliveryResponse>>();
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<DeliveryResponse>>> GetByPage(int page, int pageSize)
        {
            var blogs = await _deliveriesService.GetByPage(page, pageSize);
            var response = blogs.Adapt<List<DeliveryResponse>>();
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<DeliveryResponse>>> GetByCustomerId(int pageIndex, int pageSize, Guid customerId)
        {
            var blogs = await _deliveriesService.GetByCustomerId(pageIndex, pageSize, customerId);
            var response = blogs.Adapt<List<DeliveryResponse>>();
            return Ok(response);
        }

        // GET api/<BlogsController>/5
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<DeliveryResponse>> Get(Guid id)
        {
            var blog = await _deliveriesService.GetById(id);
            var response = blog.Adapt<BlogResponse>();
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Post(DeliveryAddRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _deliveriesService.Add(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(Guid id, [FromBody] DeliveryAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var delivery = request.Adapt<DeliveryData>();
                delivery.Id = id;
                await _deliveriesService.Update(delivery);
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
                await _deliveriesService.Delete(id);
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
                return Ok(await _deliveriesService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
