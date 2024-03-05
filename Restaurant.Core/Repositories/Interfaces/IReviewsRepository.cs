using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface IReviewsRepository
{
    Task<ICollection<Review>> GetAll();
    Task<Review?> Get(int id);
    Task<int> Create(ReviewForCreateDto dto);
    Task<bool> Update(Review dto);
    Task<bool> Delete(int id);
}