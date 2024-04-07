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
    public class CartsRepository : ICartsRepository
    {
        private readonly RestaurantDbContext _context;

        public CartsRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Cart entity)
        {
            var model = new Cart()
            {
                DishCarts = entity.DishCarts,
                Customer = entity.Customer,
                Bill = entity.Bill
            };
            await _context.Carts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var cart = await _context.Carts
                .Include(x => x.DishCarts)
                .FirstOrDefaultAsync();
            await _context.DishCarts
                .Where(x => x.CartId == id).ExecuteDeleteAsync();
            return await _context.Carts
                .Where(x => x.Id == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<ICollection<Cart>> GetAll()
        {
            return await _context.Carts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cart> GetById(Guid id)
        {
            return await _context.Carts.AsNoTracking()
                .Include(x => x.DishCarts)
                .Include(x => x.Customer)
                .Include(x => x.Bill)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException("Cuisine not found!");
        }

        public async Task<ICollection<Cart>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Carts.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            await _context.DishCarts
                .Where(d => values.Contains(d.CartId))
                .ExecuteDeleteAsync();

            return await _context.Carts
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Cart entity)
        {
            return await _context.Carts
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.DishCarts, entity.DishCarts)
                    .SetProperty(r => r.Customer, entity.Customer)
                    .SetProperty(r => r.Bill, entity.Bill)) == 1;
        }
    }
}
