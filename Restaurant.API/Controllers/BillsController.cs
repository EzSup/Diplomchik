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
using Restaurant.Core.Dtos.Bill;

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
        [Authorize(Policy ="AdminPolicy")]
        public async Task<ActionResult<ICollection<BillResponse>>> GetAll()
        {
            var blogs = await _billsService.GetAll();
            var response = blogs.Adapt<List<BillResponse>>();
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<ICollection<BillResponse>>> GetByPage(int page, int pageSize)
        {
            var blogs = await _billsService.GetByPage(page, pageSize);
            var response = blogs.Adapt<List<BillResponse>>();
            return Ok(response);
        }

        // GET api/<BlogsController>/5
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<BillResponse>> Get(Guid id)
        {
            var bill = await _billsService.GetResponseById(id);
            return Ok(bill);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<BillResponse>> GetBillsOfCustomer(int pageIndex, int pageSize, Guid customerId)
        {
            return Ok(await _billsService.GetBillsOfCustomer(pageIndex, pageSize, customerId));
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
                return Ok(await _billsService.Add(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<BillResponse>> RegisterBill(BillAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _billsService.RegisterBill(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<decimal>> Pay(BillPayRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _billsService.Pay(request.BillId, request.Amount, request.TipsPercents);
                if (result.flag)
                {
                    return Ok(result.rest);
                }
                return BadRequest(result.message);
                
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
