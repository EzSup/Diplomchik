using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Services.Interfaces;

public interface ICustomersService : ICustomersRepository
{
    Task<Customer?> LogIn(LogInData data);

    record LogInData(string email, string password);
}