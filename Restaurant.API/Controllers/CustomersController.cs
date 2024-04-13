using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.API.Contracts.Customers;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAll()
        {
            var customers = await _customersService.GetAll();
            var response = customers.Adapt<IEnumerable<CustomerResponse>>();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerResponse>> Get(Guid id)
        {
            var customer = await _customersService.GetById(id);
            var response = customer.Adapt<CustomerResponse>();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerResponse>> GetByUser(Guid id)
        {
            var customer = await _customersService.GetByUser(id);
            var response = customer.Adapt<CustomerResponse>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody]CustomerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = new Customer()
            {
                Name = request.Name,
                UserId = request.UserId,
                CartId = request.CartId ?? Guid.Empty
            };
            var result = await _customersService.Add(customer);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> Put(Guid id, [FromBody] CustomerUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var customer = request.Adapt<Customer>();
                customer.Id = id;
                await _customersService.Update(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult> UpdateImage(Guid id, string ImageLink)
        {
            if (!ModelState.IsValid || String.IsNullOrWhiteSpace(ImageLink))
            {
                return BadRequest();
            }
            try
            {
                if(await _customersService.UpdateImage(id, ImageLink))
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _customersService.Delete(id);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
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
                return Ok(await _customersService.Purge(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
