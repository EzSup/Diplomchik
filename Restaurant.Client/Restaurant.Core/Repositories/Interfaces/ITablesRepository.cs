using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
namespace Restaurant.Core.Repositories.Interfaces;

public interface ITablesRepository
{
    Task<ICollection<Table>> GetAll();
    Task<Table?> Get(int id);
    Task<int> Create(TableForCreateDto dto);
    Task<bool> Update(TableDto dto);
    Task<bool> Delete(int id);
}