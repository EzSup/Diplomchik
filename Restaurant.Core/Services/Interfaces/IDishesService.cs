using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Services.Interfaces;

public interface IDishesService : IDishesRepository
{
    Task<ICollection<Dish>> GetAvailable();
    Task<ICollection<Dish>> GetDishesOnSale();
    Task<int> AddDiscount(int dishId, double discountSize);
}