using Restaurant.Core.Dtos.Dish;
using Restaurant.Core.Enums;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IDishesRepository : ICrudRepository<Dish>
    {
        Task<ICollection<Dish>> GetByPageAvailable(int page, int pageSize);
        Task<ICollection<DishPaginationResponse>> GetByFilter(DishSortingOrder order,string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0
            );
    }
}
