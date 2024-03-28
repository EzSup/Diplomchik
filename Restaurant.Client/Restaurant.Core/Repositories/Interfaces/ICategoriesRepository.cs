using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface ICategoriesRepository : ICRUDRepo<Category, CategoryForCreateDto>
{
}