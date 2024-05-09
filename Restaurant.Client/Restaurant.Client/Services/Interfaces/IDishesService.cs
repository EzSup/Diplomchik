using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Client.Contracts.Dishes;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IDishesService
    {
        Task<ICollection<DishResponse>> GetAllDishes();
        Task<DishResponse> GetById(Guid id);
        Task<string> Add(IEnumerable<IBrowserFile> files, DishCreateRequest request);
        Task<bool> Update(IEnumerable<IBrowserFile> files,DishRequest request);
        Task<bool> Delete(Guid id);
        Task<ICollection<DishPaginationResponse>> GetByFilter(DishPaginationRequest request);
        Task<int> GetPagesCount(DishPaginationRequest request);
        Task<bool> AddDiscount(Guid dishId, double PercentsAmount);
        Task<bool> RemoveDiscount(Guid dishId);
        Task<DishDataPageResponse> GetDishDataPageById(Guid id);
        Task<bool> RemoveImage(string imageLink, DishRequest request);
    } 
}
