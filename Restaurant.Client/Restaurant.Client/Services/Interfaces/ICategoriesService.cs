using Restaurant.Client.Contracts.Categories;
using Restaurant.Client.Contracts.Cuisines;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<ICollection<CategoryResponse>> GetAll();
        Task<string> Add(CategoryCreateRequest request);
        Task<bool> Update(CategoryRequest request);
        Task<bool> Delete(Guid id);
        Task<bool> AddDiscount(Guid categoryId, double PercentsAmount);
        Task<bool> RemoveDiscount(Guid categoryId);
    }
}
