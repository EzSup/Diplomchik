using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using static Restaurant.Core.DTOs.Responses.CustomResponses;

namespace Restaurant.Core.Services.Interfaces;

public interface ICustomersService : ICustomersRepository
{
    Task<LogoChangingResponse> UpdateLogo(int customerId, string photoPath);
}