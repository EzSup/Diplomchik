using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using static Dapper.SqlMapper;

namespace Restaurant.Core.Interfaces
{
    public interface ICartsRepository 
    {
        Task<ICollection<CartResponse>> GetAll();
        Task<CartResponse> GetById(Guid id);
        Task<Guid> Add(Cart entity);
        Task<bool> Update(Cart entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<CartResponse>> GetByPage(int page, int pageSize);
        Task<CartResponse> GetByCustomerId(Guid id);
    }
}
