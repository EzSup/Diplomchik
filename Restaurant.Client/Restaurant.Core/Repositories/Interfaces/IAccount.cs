using Restaurant.Core.DTOs.SmallDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant.Core.DTOs.Responses.CustomResponses;

namespace Restaurant.Core.Repositories.Interfaces
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDto model);
        Task<LoginResponse> LoginAsync(LoginDto model);
    }
}
