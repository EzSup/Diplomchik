using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface IDishBillsRepository
{
    Task<ICollection<DishBill>> GetAll();
    Task<DishBill?> Get(int id);
    Task<int> Create(DishBillForCreateDto dto);
    Task<bool> Update(DishBill dto);
    Task<bool> Delete(int id);
}