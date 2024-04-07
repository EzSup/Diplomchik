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
    public class DiscountsRepository : IDiscountsRepository
    {
        private readonly RestaurantDbContext _context;
        public DiscountsRepository(RestaurantDbContext context) 
        { 
            _context = context;
        }

        public async Task<Guid> Add(Discount entity)
        {
            var model = new Discount()
            {
                PecentsAmount = entity.PecentsAmount
            };
            await _context.Discounts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Discounts
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Discount>> GetAll()
        {
            return await _context.Discounts
                .AsNoTracking()
                .OrderBy(x => x.PecentsAmount)
                .ToListAsync();
        }

        public async Task<Discount> GetById(Guid id)
        {
            return await _context.Discounts.AsNoTracking()
                .Include(x => x.Categories)
                .Include(x => x.Cuisine)
                .Include(x => x.Dishes) 
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Discount not found!");
        }

        public async Task<ICollection<Discount>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Discounts.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new NotImplementedException();
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Discounts
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Discount entity)
        {
            return await _context.Discounts
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.PecentsAmount, entity.PecentsAmount)) == 1;
        }
    }
}
