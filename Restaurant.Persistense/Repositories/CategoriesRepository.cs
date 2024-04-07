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
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly RestaurantDbContext _context;

        public CategoriesRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Category entity)
        {
            var model = new Category()
            {
                Name = entity.Name,
                DiscountId = entity.DiscountId
            };
            await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Categories
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _context.Categories.AsNoTracking()
                .Include(x => x.Discount)
                .Include(x => x.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Category not found!");
        }

        public async Task<ICollection<Category>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Categories.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new NotImplementedException();
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Categories
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Category entity)
        {
            return await _context.Categories
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, entity.Name)
                    .SetProperty(r => r.DiscountId, entity.DiscountId)) == 1;
        }
    }
}
