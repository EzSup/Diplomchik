using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using Mapster;
using Restaurant.API.Contracts.Bills;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillsService _billsService;

        public BillsController(IBillsService billsService)
        {
            _billsService = billsService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<BillResponse>>> GetAll()
        {
            var blogs = await _billsService.GetAll();
            var response = blogs.Adapt<List<BillResponse>>();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<BillResponse>>> GetByPage(int page, int pageSize)
        {
            var blogs = await _billsService.GetByPage(page, pageSize);
            var response = blogs.Adapt<List<BillResponse>>();
            return Ok(response);
        }

        // GET api/<BlogsController>/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BillResponse>> Get(Guid id)
        {
            var blog = await _billsService.GetById(id);
            var response = blog.Adapt<BillResponse>();
            return Ok(response);
        }

        // POST api/<BlogsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Post(BillAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var blog = new Bill()
                {
                    PaidAmount = request.PaidAmount,
                    TipsPercents = request.TipsPercents,
                    CustomerId = request.CustomerId,
                    Type = request.Type,
                    ReservationId = request.Type == Bill.OrderType.InRestaurant ? request.ReservationOrDeliveryId : Guid.Empty,
                    DeliveryId = request.Type == Bill.OrderType.Delivery ? request.ReservationOrDeliveryId : Guid.Empty,
                };
                return Ok(await _billsService.Add(blog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BlogsController>/5
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _billsService.Delete(id);
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
                return Ok(await _billsService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
