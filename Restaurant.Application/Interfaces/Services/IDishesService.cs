using Restaurant.Core.Dtos;
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
        Task<Guid> Add(Dish entity);
        Task<bool> Update(Dish entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Dish>> GetByPage(int page, int pageSize);
        Task<ICollection<DishPaginationResponse>> GetByFilter(string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0);

        Task<ICollection<DishPaginationResponse>> GetByFilterPage(int page, int pageSize, string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0);

        Task<int> PagesCount(int pageSize, string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0);
    }
}
