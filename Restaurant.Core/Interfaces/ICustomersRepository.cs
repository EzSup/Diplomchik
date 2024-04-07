using Restaurant.Core.Models;

namespace Restaurant.Core.Interfaces
{
    public interface ICustomersRepository : ICrudRepository<Customer>
    {
        Task<ICollection<Customer>> GetByFilter(string? Name,
            Guid? UserId,
            Guid? CartId,
            Guid? ReviewId,
            Guid? BillId);
    }
}
