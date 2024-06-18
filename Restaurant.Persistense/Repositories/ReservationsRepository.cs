using Microsoft.AspNetCore.Mvc.Routing;
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
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly RestaurantDbContext _context;

        public ReservationsRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Reservation entity)
        {
            entity.Start = new DateTime(entity.Start.Year,
                entity.Start.Month, entity.Start.Day,
                entity.Start.Hour, 0, 0);

            if(entity.Start < DateTime.Today)
            {
                throw new ArgumentException("Не можна резервувати столик раніше ніж на сьогодні!");
            }

            entity.Start = DateTime.SpecifyKind(entity.Start, DateTimeKind.Utc);

            var reservation = new Reservation()
            {
                TableId = entity.TableId,
                Start = entity.Start,
                End = entity.Start.AddHours(1)
            };
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Reservations
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Reservation>> GetAll()
        {
            return await _context.Reservations.AsNoTracking().ToListAsync();
        }

        public async Task<Reservation> GetById(Guid id)
        {
            return await _context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) 
                ?? throw new KeyNotFoundException("No reservation with this id!");
        }

        public async Task<ICollection<Reservation>> GetByPage(int page, int pageSize)
        {
            if (page > 0 && pageSize > 0)
            {
                return await _context.Reservations.AsNoTracking()
                    .Skip(pageSize * page - 1)
                    .Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<ICollection<Reservation>> GetByFilter(DateTime startDateFrom, DateTime endDateTill = new DateTime(), Guid? tableId = null)
        {
            startDateFrom = DateTime.SpecifyKind(startDateFrom, DateTimeKind.Utc);
            endDateTill = DateTime.SpecifyKind(endDateTill, DateTimeKind.Utc);
            var query = _context.Reservations.AsNoTracking();
            if(tableId != null && tableId != Guid.Empty)
            {
                query = query.Where(x => x.TableId == tableId);
            }
            if(startDateFrom < endDateTill)
            {
                query = query.Where(x => x.Start >= startDateFrom && x.End <= endDateTill);
            }

            return await query
                .Select(x => new Reservation()
                {
                    Id = x.Id,                    
                    Start = x.Start,
                    End = x.End,
                    TableId = x.TableId,
                }).ToListAsync();
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Reservations
                .Where(x => values.Contains(x.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Reservation entity)
        {
            return await _context.Reservations
                .ExecuteUpdateAsync(x => x
                    .SetProperty(r => r.Start, entity.Start)
                    .SetProperty(r => r.End, entity.End)
                    .SetProperty(r => r.TableId, entity.TableId)) == 1;
        }
    }
}
