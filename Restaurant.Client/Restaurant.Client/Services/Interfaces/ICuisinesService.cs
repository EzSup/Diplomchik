using Restaurant.Client.Contracts.Categories;
using Restaurant.Client.Contracts.Cuisines;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICuisinesService
    {
        Task<ICollection<CuisineResponse>> GetAll();
        Task<string> Add(CuisineCreateRequest request);
        Task<bool> Update(CuisineRequest request);
        Task<bool> Delete(Guid id);
        Task<bool> AddDiscount(Guid cuisineId, double PercentsAmount);
        Task<bool> RemoveDiscount(Guid cuisineId);
    }
}
