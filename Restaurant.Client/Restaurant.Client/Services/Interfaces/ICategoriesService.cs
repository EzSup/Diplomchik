using Restaurant.Client.Contracts.Categories;
using Restaurant.Client.Contracts.Cuisines;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<ICollection<CategoryResponse>> GetAll();
        Task<string> Add(CategoryCreateRequest request);
    }
}
