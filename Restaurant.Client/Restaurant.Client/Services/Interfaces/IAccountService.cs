using Restaurant.Core.DTOs.SmallDtos;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Core.DTOs.Responses.CustomResponses;

namespace Restaurant.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDto model);
        Task<LoginResponse> LoginAsync(LoginDto model);
    }
}
