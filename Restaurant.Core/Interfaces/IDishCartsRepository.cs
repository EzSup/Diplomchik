using Restaurant.Core.Models;

namespace Restaurant.Core.Interfaces
{
    public interface IDishCartsRepository : ICrudRepository<DishCart>
    {
        Task<ICollection<DishCart>> GetByFilter(Guid? CartId = null, Guid? DishId = null);
    }
}
