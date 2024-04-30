using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IReviewsRepository : ICrudRepository<Review>
    {
        Task<ICollection<Review>> GetByFilter(int pageIndex, int pageSize, Guid? DishId = null, Guid? AuthorId = null, double minRate = 1, double maxRate = 5);
        Task<ICollection<Review>> GetByDishId(Guid dishId);
    }
}
