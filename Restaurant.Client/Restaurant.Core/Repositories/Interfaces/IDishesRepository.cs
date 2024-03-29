using Restaurant.Core.DTOs;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface IDishesRepository : ICRUDRepo<Dish, DishForCreateDto>
{
}