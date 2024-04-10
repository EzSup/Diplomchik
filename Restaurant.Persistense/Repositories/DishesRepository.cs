using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Persistense.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private readonly RestaurantDbContext _context;

        public DishesRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Dish entity)
        {
            var model = new Dish()
            {
                Name = entity.Name,
                Weight = entity.Weight,
                IngredientsList = entity.IngredientsList,
                Available = entity.Available,
                Price = entity.Price,
                PhotoLinks = entity.PhotoLinks,
                DiscountId = entity.DiscountId,
                CategoryId = entity.CategoryId,
                CuisineId = entity.CuisineId,
            };
            await _context.Dishes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Dishes
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Dish>> GetAll()
        {
            return await _context.Dishes.AsNoTracking()
                .OrderBy(x => x.CategoryId)
                .ToListAsync();
        }

        public async Task<ICollection<Dish>> GetByFilter(string? Name = null, 
            double MinWeight = 0, 
            double MaxWeight = double.MaxValue, 
            IEnumerable<string>? Ingredients = null, 
            bool? Available = null, 
            decimal MinPrice = 0, 
            decimal MaxPrice = decimal.MaxValue, 
            string? Category = null, 
            string? Cuisine = null, 
            double MinDiscountsPercents = 0)
        {
            var query = _context.Dishes.AsNoTracking();

            if(!string.IsNullOrWhiteSpace(Name))
                query = query.Where(x => x.Name.Contains(Name));

            if(MinWeight < MaxWeight && MinWeight >= 0)
                query = query.Where(x => x.Weight > MinWeight && x.Weight < MaxWeight);

            if(Ingredients != null && Ingredients.Count() > 0)
                query = query.Where(x => Ingredients.All(c => x.IngredientsList.Contains(c)));

            if(Available != null)
                query = query.Where(x => x.Available == Available);

            if (MinPrice < MaxPrice && MinPrice >= 0)
                query = query.Where(x => x.Price > MinPrice && x.Price < MaxPrice);

            if (!string.IsNullOrWhiteSpace(Category))
                query = query.Where(x => x.Category.Name.Contains(Category));

            if (!string.IsNullOrWhiteSpace(Cuisine))
                query = query.Where(x => x.Cuisine.Name.Contains(Cuisine));

            if(MinDiscountsPercents > 0)
                query = query.Where(x => x.Discount.PecentsAmount > MinDiscountsPercents);

            return await query.ToListAsync();
        }

        public async Task<Dish> GetById(Guid id)
        {
            return await _context.Dishes
                .AsNoTracking()
                .Include(x => x.DishCarts)
                .Include(x => x.Reviews)
                .Include(x => x.Cuisine)
                .Include(x => x.Category)
                .Include(x => x.Discount)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Dish not found!");
        }

        public async Task<ICollection<Dish>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Dishes.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<ICollection<Dish>> GetByPageAvailable (int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Dishes.AsNoTracking()
                    .Where(x => x.Available)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Dishes
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Dish entity)
        {
            return await _context.Dishes
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, entity.Name)
                    .SetProperty(r => r.Weight, entity.Weight)
                    .SetProperty(r => r.IngredientsList, entity.IngredientsList)
                    .SetProperty(r => r.Available, entity.Available)
                    .SetProperty(r => r.Price, entity.Price)
                    .SetProperty(r => r.PhotoLinks, entity.PhotoLinks)
                    .SetProperty(r => r.DiscountId, entity.DiscountId)
                    .SetProperty(r => r.CuisineId, entity.CuisineId)
                    .SetProperty(r => r.CategoryId, entity.CategoryId)) == 1;
        }
    }
}
