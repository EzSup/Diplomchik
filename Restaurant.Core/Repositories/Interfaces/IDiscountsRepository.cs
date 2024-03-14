using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface IDiscountsRepository
{
    Task<ICollection<Discount>> GetAll();
    Task<Discount?> Get(int id);
    Task<int> Create(DiscountForCreateDto dto);
    Task<bool> Update(DiscountDto dto);
    Task<bool> Delete(int id);
}