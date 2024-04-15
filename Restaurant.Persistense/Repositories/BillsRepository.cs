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
    public class BillsRepository : IBillsRepository
    {
        private readonly RestaurantDbContext _context;

        public BillsRepository(RestaurantDbContext context)
        {
            _context = context;
        }       

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Bills
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Bill>> GetAll()
        {
            return await _context.Bills.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Bill>> GetByFilter(int pageIndex, int pageSize,decimal MinPrice = 0, decimal MaxPrice = decimal.MaxValue, 
            DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null, 
            Bill.OrderType? orderType = null, int minTipsPercents = 0, int maxTipsPercents = 100,
            Guid? customerId = null, Guid? reservationId = null, Guid? deliveryId = null)
        {
            var query = _context.Bills.AsNoTracking();
            if(MinPrice >0 && MinPrice < MaxPrice)
            {
                query = query.Where(x => x.TotalPrice > MinPrice && x.TotalPrice < MaxPrice);
            }
            if(minOrderDateTime != null && maxOrderDateTime != null) 
            {
                query = query.Where(x => x.OrderDateAndTime > minOrderDateTime && x.OrderDateAndTime < maxOrderDateTime);
            }
            if(orderType != null)
            {
                query = query.Where(x => x.Type == orderType);
            }
            if(minTipsPercents > 0 && minTipsPercents < maxTipsPercents)
            {
                query = query.Where(x => x.TipsPercents > minTipsPercents && x.TipsPercents < maxTipsPercents);
            }
            if(customerId != null)
            {
                query = query.Where(x => x.CustomerId == customerId);
            }
            if (reservationId != null)
            {
                query = query.Where(x => x.ReservationId == reservationId);
            }
            if (deliveryId != null)
            {
                query = query.Where(x => x.DeliveryId == deliveryId);
            }

            if (pageIndex > 0 && pageSize > 0)
            {
                return await query
                    .Skip(pageSize * pageIndex - 1)
                    .Take(pageSize)
                    .Select(x => new Bill()
                    {
                        Id = x.Id,
                        TotalPrice = x.TotalPrice,
                        PaidAmount  = x.PaidAmount,
                        OrderDateAndTime = x.OrderDateAndTime,
                        TipsPercents = x.TipsPercents,
                        Type = x.Type,
                        CustomerId = x.CustomerId,
                        CartId = x.CartId,
                        ReservationId = x.ReservationId,
                        DeliveryId = x.DeliveryId
                    }).ToListAsync();
            }

            throw new ArgumentException("Page size and number has to be greater than 0!");

        }

        public async Task<Bill> GetById(Guid id)
        {
            return await _context.Bills
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("No bills with this id!");
        }

        public async Task<ICollection<Bill>> GetByPage(int page, int pageSize)
        {
            if (page > 0 && pageSize > 0)
            {
                return await _context.Bills.AsNoTracking()
                    .Skip(pageSize * page - 1)
                    .Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Bills
                .Where(x => values.Contains(x.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Bill entity)
        {
            var totalPrice = await CalcPrice(entity.CartId);

            return await _context.Bills
                .ExecuteUpdateAsync(x => x
                    .SetProperty(r => r.TotalPrice, totalPrice)
                    .SetProperty(r => r.PaidAmount, entity.PaidAmount)
                    .SetProperty(r => r.OrderDateAndTime, entity.OrderDateAndTime)
                    .SetProperty(r => r.TipsPercents, entity.TipsPercents)
                    .SetProperty(r => r.Type, entity.Type)
                    .SetProperty(r => r.CustomerId, entity.CustomerId)
                    .SetProperty(r => r.CartId, entity.CartId)
                    .SetProperty(r => r.ReservationId, entity.ReservationId)
                    .SetProperty(r => r.DeliveryId, entity.DeliveryId)) == 1;
        }        

        public async Task<Guid> Add(Bill entity)
        {
            if (entity.Type == null)
            {
                throw new ArgumentException("Type has to be set!");
            }
            if (!await _context.Reservations.AnyAsync(x => x.Id == entity.ReservationId)
                && !await _context.DeliveryDatas.AnyAsync(x => x.Id == entity.DeliveryId))
            {
                throw new ArgumentException("Reservation or delivery has to be set!");
            }

            var totalPrice = await CalcPrice(entity.CartId);
            var sum = totalPrice + totalPrice * entity.TipsPercents;

            if(sum > entity.PaidAmount)
            {
                throw new ArgumentException("Resulting sum is more than paid amount!");
            }

            Guid CartId = (await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == entity.CustomerId)).Id;

            if(CartId == Guid.Empty)
            {
                throw new KeyNotFoundException("Cart not found");
            }

            var bill = new Bill()
            {
                TotalPrice = totalPrice,
                PaidAmount = entity.PaidAmount,
                OrderDateAndTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                TipsPercents = entity.TipsPercents,
                Type = entity.Type,
                CustomerId = entity.CustomerId,
                CartId = CartId,
                ReservationId = entity.ReservationId,
                DeliveryId = entity.DeliveryId
            };

            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
            return bill.Id;
        }

        private async Task<decimal> CalcPrice(Guid cartId)
        {
            return await _context.Carts
                .Where(x => x.Id == cartId)
                .AsNoTracking()
                .Include(x => x.DishCarts)
                .ThenInclude(dc => dc.Dish)
                .SumAsync(x => x.DishCarts.Sum(dc => dc.Dish.Price * dc.Count));
        }
    }
}
