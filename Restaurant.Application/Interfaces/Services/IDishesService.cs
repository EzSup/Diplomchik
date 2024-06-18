using Restaurant.Core.Dtos.Dish;
using Restaurant.Core.Enums;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IDishesService
    {
        Task<ICollection<Dish>> GetAll();
        Task<Dish> GetById(Guid id);
        Task<DishDataPageResponse> GetDishDataById(Guid id);
        Task<Guid> Add(Dish entity);
        Task<bool> Update(Dish entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Dish>> GetByPage(int page, int pageSize);
        Task<ICollection<DishPaginationResponse>> GetByFilter(DishPaginationRequest request);

        //Task<ICollection<DishPaginationResponse>> GetByFilterPage(DishPaginationRequest request);

        Task<int> PagesCount(DishPaginationRequest request);
        Task<bool> AddDiscount(Guid dishId, double PercentsAmount);
        Task<bool> RemoveDiscount(Guid dishId);
    }
}
