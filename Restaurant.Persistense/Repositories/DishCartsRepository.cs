using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
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

        public async Task<bool> Delete(Guid cartId, Guid dishId)
        {
            return await _context.DishCarts
                .Where(x => x.CartId == cartId && x.DishId == dishId)
                .ExecuteDeleteAsync() > -1;
        }

        public async Task<ICollection<DishCart>> GetAll()
        {
            return await _context.DishCarts
                .AsNoTracking()
                .OrderBy(x => x.CartId)
                .ToListAsync();
        }

        public async Task<ICollection<DishCart>> GetByFilter(Guid? CartId = null, Guid? DishId = null, int minCount = 1, int maxCount = int.MaxValue)
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
            query = query.Where(x => x.Count >= minCount && x.Count <= maxCount);
            return await query.ToListAsync();
        }

        public async Task<DishCart> GetById(DishCartId Id)
        {
            return await _context.DishCarts
                .AsNoTracking()
                .Include(x => x.Cart)
                .Include(x => x.Dish)
                .FirstOrDefaultAsync(x => x.CartId == Id.CartId && x.DishId == Id.DishId) 
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

        public async Task<int> Purge(IEnumerable<DishCartId> values)
        {
            return await _context.DishCarts
                .Where(x => values.Select(v => v.CartId).Contains(x.CartId)
                    && values.Select(v => v.DishId).Contains(x.DishId))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(DishCart entity)
        {
            try
            {
                return await _context.DishCarts
                    .Where(x => x.DishId == entity.DishId && x.CartId == entity.CartId)
                    .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.Count, entity.Count)) > 0;
            }
            catch {
                return false;
            }
        }
    }
}
