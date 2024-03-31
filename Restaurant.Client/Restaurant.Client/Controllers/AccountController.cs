using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.DTOs.SmallDtos;
using Restaurant.Core.Repositories.Interfaces;
using static Restaurant.Core.DTOs.Responses.CustomResponses;

namespace Restaurant.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountRepository;

        public AccountController(IAccount accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDto model)
        {
            var result = await _accountRepository.RegisterAsync(model);
            if(!result.Flag)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<RegistrationResponse>> LoginAsync(LoginDto model)
        {
            var result = await _accountRepository.LoginAsync(model);
            if (!result.Flag)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
