using Restaurant.Client.Contracts.Reviews;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IReviewsService
    {
        Task<ICollection<ReviewOfDishResponse>> GetByDishId(Guid dishId, int pageIndex, int pageSize);
        Task<bool> Post(ReviewCreateRequest request);
        Task<ReviewOfDishResponse?> GetOwnReviewOnDish(Guid dishId);
        Task<(bool ifHas, ReviewOfDishResponse? obj)> HasOwnReview(Guid dishId);
    }
}
