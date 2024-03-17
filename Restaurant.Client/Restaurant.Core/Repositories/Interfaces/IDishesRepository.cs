using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface IDishesRepository
{
    Task<ICollection<Dish>> GetAll();
    Task<Dish?> Get(int id);
    Task<int> Create(DishForCreateDto dto);
    Task<bool> Update(DishDto dto);
    Task<bool> Delete(int id);
}