using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Contracts.Blogs;
using Restaurant.API.Contracts.Carts;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Dtos.Сart;
using Restaurant.Core.Interfaces;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartService;

        public CartsController(ICartsService cartsService)
        {
            _cartService = cartsService;
        }


        [HttpGet("{customerId:guid}")]
        [Authorize]
        public async Task<ActionResult<CartResponse>> GetByCustomer(Guid customerId)
        {
            var cart = await _cartService.GetByCustomerId(customerId);
            return Ok(cart);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<CartResponse>> Get(Guid id)
        {
            var cart = await _cartService.GetById(id);
            return Ok(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartResponse>> AddDishToCartOfUser([FromBody] AddDishRequest request)
        {
            var cart = await _cartService.AddDishToUsersCart(request.customerId, request.dishId, request.count);
            return Ok(cart);
        }

    }
}
