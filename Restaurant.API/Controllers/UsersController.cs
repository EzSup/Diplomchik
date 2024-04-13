using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Restaurant.API.Contracts.Users;
using Restaurant.Application.Interfaces.Services;
using Mapster;
using Restaurant.Core.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IUsersService usersService, IHttpContextAccessor httpContextAccessor, ICustomersService customersService)
        {
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterUserResponse>> Register([FromBody]RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new RegisterUserResponse(false, "Дані некоректні"));
            }
            try
            {
                var response = new RegisterUserResponse(true, "Успішно зареєстровано!", 
                    await _usersService.Register(request.email, request.password, request.phoneNumber));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new RegisterUserResponse(false, ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginUserResponse>> Login([FromBody]LoginUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginUserResponse(false, "Invalid state"));
            }
            try
            {
                var token = await _usersService.Login(request.email, request.password);

                _httpContextAccessor.HttpContext.Response.Cookies.Append("tasty-cookies", token);

                return Ok(new LoginUserResponse(true, null, token));
            }
            catch (Exception ex)
            {
                return BadRequest(new LoginUserResponse(false, ex.Message));
            }
        }

        [HttpDelete]
        public ActionResult Logout()
        {
            try
            {
                //_httpContextAccessor.HttpContext.Response.Cookies.Append("tasty-cookies", "");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("tasty-cookies");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
