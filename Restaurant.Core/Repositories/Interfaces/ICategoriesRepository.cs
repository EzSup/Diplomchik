using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface ICategoriesRepository
{
    Task<ICollection<Category>> GetAll();
    Task<Category?> Get(int id);
    Task<int> Create(CategoryForCreateDto dto);
    Task<bool> Update(Category dto);
    Task<bool> Delete(int id);
}