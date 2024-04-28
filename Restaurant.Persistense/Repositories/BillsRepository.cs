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



        //public async Task<Bill> GetById(Guid id)
        //{
        //    var bill = await _context.Bills
        //        .Include(x => x.Cart)
        //        .ThenInclude(c => c.DishCarts)
        //        .ThenInclude(dc => dc.Dish)
        //        .AsNoTracking()
        //        .Select(x => new Bill()
        //        {
        //            Id = x.Id,
        //            Cart = new Cart()
        //            {
        //                Id = x.CartId,
        //            },
        //            CartId = x.CartId,
        //            CustomerId = x.CustomerId,
        //            DeliveryId = x.DeliveryId,
        //            ReservationId = x.ReservationId,
        //            OrderDateAndTime = x.OrderDateAndTime,
        //            IsPaid = x.IsPaid,
        //            PaidAmount = x.PaidAmount,
        //            Tips = x.Tips,
        //            TotalPrice = x.TotalPrice,
        //            Type = x.Type,
        //        })
        //        .SingleOrDefaultAsync(x => x.Id == id);

        //    var dishCarts = await _context.DishCarts.Include(dc => dc.Dish).AsNoTracking().Where(x => x.CartId == bill.CartId)
        //        .Select(x => new DishCart()
        //        {
        //            CartId = x.CartId,
        //            DishId = x.DishId,
        //            Count = x.Count
        //        }).ToListAsync();
        //    var dishIds = dishCarts.Select(dc => dc.DishId).ToList();
        //    var dishes = await _context.Dishes
        //        .ToListAsync();
        //    dishes = dishes.Where(x => dishIds.Any(id => id == x.Id)).ToList();

        //    foreach (var dishCart in dishCarts)
        //    {
        //        dishCart.Dish = dishes.FirstOrDefault(x => x.Id == dishCart.DishId);
        //    }

        //    bill.Cart.DishCarts = dishCarts;

        //    if (bill == null)
        //    {
        //        throw new KeyNotFoundException("No bills with this id!");
        //    }

        //    return bill;
        //}

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

            //entity.Cart = _context.Carts.FirstOrDefault(x => x.Id == entity.CartId);
            //entity.Reservation = _context.Reservations.FirstOrDefault(x => x.Id == entity.ReservationId);
            //entity.DeliveryData = _context.DeliveryDatas.FirstOrDefault(x => x.Id == entity.DeliveryId);

            //return await _context.Bills
            //    .ExecuteUpdateAsync(x => x
            //        .SetProperty(r => r.TotalPrice, totalPrice)
            //        .SetProperty(r => r.PaidAmount, entity.PaidAmount)
            //        //.SetProperty(r => r.OrderDateAndTime, entity.OrderDateAndTime.ToUniversalTime())
            //        .SetProperty(r => r.Tips, entity.Tips)
            //        .SetProperty(r => r.Type, entity.Type)
            //        .SetProperty(r => r.CustomerId, entity.CustomerId)
            //        .SetProperty(r => r.CartId, entity.CartId)
            //        .SetProperty(r => r.Cart, entity.Cart)
            //        .SetProperty(r => r.ReservationId, entity.ReservationId)
            //        .SetProperty(r => r.Reservation, entity.Reservation)
            //        .SetProperty(r => r.DeliveryId, entity.DeliveryId)
            //        .SetProperty(r => r.DeliveryData, entity.DeliveryData)
            //        .SetProperty(r => r.IsPaid, entity.IsPaid)) == 1;
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

            //Guid? CartId = (await _context.Customers
            //    .FirstOrDefaultAsync(x => x.Id == entity.CustomerId)).CartId;

            //if (CartId == null || CartId == Guid.Empty)
            //{
            //    throw new KeyNotFoundException("Cart not found");
            //}


            //var sum = totalPrice + totalPrice * entity.TipsPercents;

            //if(sum > entity.PaidAmount)
            //{
            //    throw new ArgumentException("Resulting sum is more than paid amount!");
            //}            

            var totalPrice = await CalcPrice(entity.CartId);

            var bill = new Bill()
            {
                TotalPrice = totalPrice,
                IsPaid = false,
                //PaidAmount = entity.PaidAmount,
                OrderDateAndTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                //TipsPercents = entity.TipsPercents,
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
