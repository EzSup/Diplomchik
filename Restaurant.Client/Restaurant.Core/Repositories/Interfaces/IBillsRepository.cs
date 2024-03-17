using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface IBillsRepository
{
    Task<ICollection<Bill>> GetAll();
    Task<Bill?> Get(int id);
    Task<int> Create(BillForCreateDto dto);
    Task<bool> Update(Bill dto);
    Task<bool> Delete(int id);
}