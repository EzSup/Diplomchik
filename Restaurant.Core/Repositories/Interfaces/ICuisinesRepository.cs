using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface ICuisinesRepository
{
    Task<ICollection<Cuisine>> GetAll();
    Task<Cuisine?> Get(int id);
    Task<int> Create(CuisineForCreateDto dto);
    Task<bool> Update(CuisineDto dto);
    Task<bool> Delete(int id);
}