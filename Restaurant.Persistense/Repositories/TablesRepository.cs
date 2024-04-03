
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

        public async Task<ICollection<Table>> GetByFilter(bool available, decimal price)
        {
            var query = _context.Tables.AsNoTracking();

            query = query.Where(t => t.Free == available);

            if(price > 0)
            {
                query = query.Where(t => t.PriceForHour > price);
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

        public async Task Add(Table obj)
        {
            var table = new Table
            {
                PriceForHour = obj.PriceForHour,
                Free = obj.Free
            };
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Table obj)
        {
            await _context.Tables
                .Where(t => t.Id == obj.Id)
                .ExecuteUpdateAsync(s =>s
                    .SetProperty(t => t.PriceForHour, obj.PriceForHour)
                    .SetProperty(t => t.Free, obj.Free));
        }

        public async Task Delete(Guid id)
        {
            await _context.Tables
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task Purge(IEnumerable<Guid> ids)
        {
            await _context.Tables
                .Where(t => ids.Contains(t.Id))
                .ExecuteDeleteAsync();
        }
    }
}
