using Restaurant.Core.Dtos.Reviews;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IReviewsService
    {
        Task<ICollection<Review>> GetAll();
        Task<Review> GetById(Guid id);
        Task<Guid> Add(Review entity);
        Task<bool> Update(Review entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Review>> GetByPage(int page, int pageSize);
        Task<ICollection<Review>> GetByFilter(int pageIndex, int pageSize, Guid? DishId = null, Guid? AuthorId = null, double minRate = 1, double maxRate = 5);
        Task<ICollection<ReviewOfDishResponse>> GetReviewsOfDish(Guid id, int pageIndex, int pageSize);
    }
}
