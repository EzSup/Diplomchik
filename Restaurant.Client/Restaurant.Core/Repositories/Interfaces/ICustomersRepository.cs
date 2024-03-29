using Restaurant.Core.DTOs;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface ICustomersRepository : ICRUDRepo<Customer, CustomerForCreateDto>
{
    Task<Customer?> Get(string email);
}