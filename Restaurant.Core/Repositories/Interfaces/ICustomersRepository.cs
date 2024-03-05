using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface ICustomersRepository
{
    Task<ICollection<Customer>> GetAll();
    Task<Customer?> Get(int id);
    Task<int> Create(CustomerForCreateDto dto);
    Task<bool> Update(Customer dto);
    Task<bool> Delete(int id);
}