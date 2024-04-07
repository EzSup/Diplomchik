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
    public class DishCartsRepository : IDishCartsRepository
    {
        private readonly RestaurantDbContext _context;

        public DishCartsRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(DishCart entity)
        {
            var model = new DishCart()
            {
                CartId = entity.CartId,
                DishId = entity.DishId,
                Count = entity.Count
            };
            await _context.DishCarts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CartId;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.DishCarts
                .Where(x => x.CartId == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<DishCart>> GetAll()
        {
            return await _context.DishCarts
                .AsNoTracking()
                .OrderBy(x => x.CartId)
                .ToListAsync();
        }

        public async Task<ICollection<DishCart>> GetByFilter(Guid? CartId = null, Guid? DishId = null)
        {
            var query = _context.DishCarts.AsNoTracking();
            if(CartId != null)
            {
                query = query.Where(x => x.CartId == CartId);
            }
            if(DishId != null)
            {
                query = query.Where(x => x.DishId == DishId);
            }
            return await query.ToListAsync();
        }

        public async Task<DishCart> GetById(Guid cartId)
        {
            return await _context.DishCarts
                .AsNoTracking()
                .Include(x => x.Cart)
                .Include(x => x.Dish)
                .FirstOrDefaultAsync(x => x.CartId == cartId) 
                    ?? throw new KeyNotFoundException("DishCart not found!");
        }

        public async Task<ICollection<DishCart>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.DishCarts.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.DishCarts
                .Where(x => values.Contains(x.CartId))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(DishCart entity)
        {
            return await _context.DishCarts
                .Where(x => x.DishId == entity.DishId && x.CartId == entity.CartId)
                .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Count, entity.Count)) > 0;
        }
    }
}
