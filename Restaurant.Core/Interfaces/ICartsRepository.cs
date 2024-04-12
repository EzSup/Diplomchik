using Restaurant.Core.Models;

namespace Restaurant.Core.Interfaces
{
    public interface ICartsRepository : ICrudRepository<Cart>
    {
        Task<Cart> GetByCustomerId(Guid id);
    }
}
