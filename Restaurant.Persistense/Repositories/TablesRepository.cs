
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Application.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Table = Restaurant.Core.Models.Table;

namespace Restaurant.Persistense.Repositories
{
    public class TablesRepository : ITablesRepository
    {
        private readonly RestaurantDbContext _context;

        public TablesRepository(RestaurantDbContext restaurantDbContext)
        {
            _context = restaurantDbContext;
        }

        public async Task<ICollection<Table>> GetAll()
        {
            return await _context.Tables
                .AsNoTracking()
                .OrderBy(t => t.PriceForHour)
                .ToListAsync();
        }

        public async Task<ICollection<Table>> GetWithBills()
        {
            return await _context.Tables
                .AsNoTracking()
                .Include(t => t.Bills)
                .ToListAsync();
        }

        public async Task<Table> GetById(Guid id)
        {
            return await _context.Tables
                .AsNoTracking()
                .Include(t => t.Bills)                
                .FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception("Table not found!");
        }

        public async Task<ICollection<Table>> GetByFilter(bool? available, decimal minPrice = 0, decimal maxPrice = decimal.MaxValue)
        {
            var query = _context.Tables.AsNoTracking();
            if(available != null)
            {
                query = query.Where(t => t.Free == available);
            }            

            if(minPrice > 0)
            {
                query = query.Where(t => t.PriceForHour > minPrice);
            }
            if (maxPrice < decimal.MaxValue)
            {
                query = query.Where(t => t.PriceForHour  < maxPrice);
            }

            return await query.ToListAsync();
        }

        public async Task<ICollection<Table>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Tables.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new NotImplementedException();
        }

        public async Task<Guid> Add(Table obj)
        {
            var table = new Table
            {
                PriceForHour = obj.PriceForHour,
                Free = obj.Free
            };
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table.Id;
        }

        public async Task<bool> Update(Table obj)
        {
            return await _context.Tables
                .Where(t => t.Id == obj.Id)
                .ExecuteUpdateAsync(s =>s
                    .SetProperty(t => t.PriceForHour, obj.PriceForHour)
                    .SetProperty(t => t.Free, obj.Free)) == 1;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Tables
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<int> Purge(IEnumerable<Guid> ids)
        {
            return await _context.Tables
                .Where(t => ids.Contains(t.Id))
                .ExecuteDeleteAsync();
        }
    }
}
