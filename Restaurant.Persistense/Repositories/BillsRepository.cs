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

        public async Task<ICollection<Bill>> GetByFilter(int pageIndex, int pageSize, decimal MinPrice = 0, decimal MaxPrice = decimal.MaxValue,
            DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null,
            Bill.OrderType? orderType = null, int minTipsPercents = 0, int maxTipsPercents = 100,
            Guid? customerId = null, Guid? reservationId = null, Guid? deliveryId = null)
        {
            var query = _context.Bills.AsNoTracking();
            if (MinPrice > 0 && MinPrice < MaxPrice)
            {
                query = query.Where(x => x.TotalPrice > MinPrice && x.TotalPrice < MaxPrice);
            }
            if (minOrderDateTime != null && maxOrderDateTime != null)
            {
                query = query.Where(x => x.OrderDateAndTime > minOrderDateTime && x.OrderDateAndTime < maxOrderDateTime);
            }
            if (orderType != null)
            {
                query = query.Where(x => x.Type == orderType);
            }
            if (minTipsPercents > 0 && minTipsPercents < maxTipsPercents)
            {
                query = query.Where(x => x.Tips > minTipsPercents && x.Tips < maxTipsPercents);
            }
            if (customerId != null)
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
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .Select(x => new Bill()
                    {
                        Id = x.Id,
                        TotalPrice = x.TotalPrice,
                        PaidAmount = x.PaidAmount,
                        OrderDateAndTime = x.OrderDateAndTime,
                        Tips = x.Tips,
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
            Bill bill = await _context.Bills
                .AsNoTracking()
                .Include(b => b.DeliveryData)
                .Include(b => b.Reservation)
                .ThenInclude(r => r.Table)
                .Include(b => b.Cart)
                .ThenInclude(c => c.DishCarts)
                .FirstAsync(x => x.Id == id);
            
            if (bill == null)
            {
                throw new KeyNotFoundException("No bills with this id!");
            }

            var dishes = await _context.Dishes
                .Where(x => bill.Cart.DishCarts.Select(dc => dc.DishId).Contains(x.Id))
                .Select(x => new Dish
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToListAsync();     

            foreach(var dc in bill.Cart.DishCarts)
            {
                dc.Dish = dishes.First(x => x.Id == dc.DishId);
            }

            return bill;
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

            var bill = await _context.Bills.FirstAsync(x => x.Id == entity.Id);

            bill.TotalPrice = totalPrice;
            bill.PaidAmount = entity.PaidAmount;
            bill.Tips = entity.Tips;
            bill.Type = entity.Type;
            bill.CustomerId = entity.CustomerId;
            bill.CartId = entity.CartId;
            bill.ReservationId = entity.ReservationId;
            bill.DeliveryId = entity.DeliveryId;
            bill.IsPaid = entity.IsPaid;
            bill.OrderDateAndTime = entity.OrderDateAndTime.ToUniversalTime();

            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> Add(Bill entity)
        {
            if (entity.Type == null)
                throw new ArgumentException("Тип не обрано!");
            if (!await _context.Reservations.AnyAsync(x => x.Id == entity.ReservationId)
                && !await _context.DeliveryDatas.AnyAsync(x => x.Id == entity.DeliveryId))
                throw new ArgumentException("Резерватія або доставка повинні бути обрані!");            
            if(entity.CustomerId == Guid.Empty)
                throw new ArgumentException("Чек повинен мати покупця!");
            if (entity.CartId == Guid.Empty)
                throw new ArgumentException("Чек повинен корзину!");       

            var totalPrice = await CalcPrice(entity.CartId);
            
            if(totalPrice <= 0)
            {
                throw new ArgumentException("Кошик пустий!");
            }            

            var bill = new Bill()
            {
                TotalPrice = totalPrice,
                IsPaid = false,
                OrderDateAndTime = DateTime.UtcNow,// .SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Type = entity.Type,
                CustomerId = entity.CustomerId,
                CartId = entity.CartId,
                ReservationId = entity.ReservationId == Guid.Empty ? null : entity.ReservationId,
                DeliveryId = entity.DeliveryId == Guid.Empty ? null : entity.DeliveryId
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
