using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Interfaces
{
    public interface IDishCartsRepository
    {
        Task<Guid> Add(DishCart entity);
        Task<bool> Update(DishCart entity);
        Task<ICollection<DishCart>> GetAll();
        Task<DishCart> GetById(DishCartId id);
        Task<ICollection<DishCart>> GetByPage(int page, int pageSize);
        Task<bool> Delete(Guid cartId, Guid dishId);
        Task<int> Purge(IEnumerable<DishCartId> values);
        Task<ICollection<DishCart>> GetByFilter(Guid? CartId = null, Guid? DishId = null, int minCount = 0, int maxCount = int.MaxValue);
    }
}
