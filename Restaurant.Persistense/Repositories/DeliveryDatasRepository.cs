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
    public class DeliveryDatasRepository : IDeliveryDatasRepository
    {
        private readonly RestaurantDbContext _context;

        public DeliveryDatasRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(DeliveryData entity)
        {
            var delivery = new DeliveryData()
            {
                Region = entity.Region,
                SettlementName = entity.SettlementName,
                StreetName = entity.StreetName,
                StreetNum = entity.StreetNum,
            };
            await _context.DeliveryDatas.AddAsync(delivery);
            await _context.SaveChangesAsync();
            return delivery.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.DeliveryDatas
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<DeliveryData>> GetAll()
        {
            return await _context.DeliveryDatas
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<DeliveryData> GetById(Guid id)
        {
            return await _context.DeliveryDatas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("No deliveries with this id!");
        }

        public async Task<ICollection<DeliveryData>> GetByPage(int page, int pageSize)
        {
            if (page > 0 && pageSize > 0)
            {
                return await _context.DeliveryDatas.AsNoTracking()
                    .Skip(pageSize * page - 1)
                    .Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }
        public async Task<ICollection<DeliveryData>> GetByFilter(int page, int pageSize, Guid? customerId = null)
        {
            var query = _context.DeliveryDatas.Include(d => d.Bill).AsNoTracking();

            if (customerId != null)
            {
                query = query.Where(x => x.Bill.CustomerId == customerId);
            }

            if (page > 0 && pageSize > 0)
            {
                return await query
                    .Skip(pageSize * page - 1)
                    .Take(pageSize)
                    .Select(x => new DeliveryData()
                    {
                        Region = x.Region,
                        SettlementName = x.SettlementName,
                        StreetName = x.StreetName,
                        StreetNum = x.StreetNum
                    }).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.DeliveryDatas
                .Where(x => values.Contains(x.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(DeliveryData entity)
        {
            return await _context.DeliveryDatas
                .ExecuteUpdateAsync(x => x
                    .SetProperty(r => r.Region, entity.Region)
                    .SetProperty(r => r.SettlementName, entity.SettlementName)
                    .SetProperty(r => r.StreetName, entity.StreetName)
                    .SetProperty(r => r.StreetNum, entity.StreetNum)) == 1;
        }
    }
}
