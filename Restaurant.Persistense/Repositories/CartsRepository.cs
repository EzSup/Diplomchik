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

        public async Task<ICollection<CartResponse>> GetAll()
        {
            return await _context.Carts
                .AsNoTracking()
                .Select(x => new CartResponse
                {
                    Id = x.Id,
                    Dishes = x.DishCarts.Select(dc => new DishOfCart(dc.Dish.Id, dc.Dish.Name, dc.Dish.PhotoLinks.First(), dc.Count)).ToList()
                })
                .ToListAsync();
        }

        public async Task<CartResponse> GetById(Guid id)
        {
            var cart = await _context.Carts.AsNoTracking()                
                .Include(x => x.Customer)
                .Include(x => x.Bill)
                .Include(x => x.DishCarts)
                .ThenInclude(dc => dc.Dish)
                .Where(x => x.Customer.Id == id)
                .Select(x => new CartResponse
                {
                    Id = x.Id,
                    Dishes = x.DishCarts.Select(dc => new DishOfCart(dc.Dish.Id, dc.Dish.Name, dc.Dish.PhotoLinks.First(), dc.Count)).ToList()
                })
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Cart not found!");
            return cart;
        }

        public async Task<CartResponse> GetByCustomerId(Guid id)
        {
            var cart = await _context.Carts.AsNoTracking()                
                .Include(x => x.Customer)
                .Include(x => x.Bill)
                .Include(x => x.DishCarts)
                .ThenInclude(dc => dc.Dish)
                .Where(x => x.Customer.Id == id)
                .Select(x => new CartResponse
                {
                    Id = x.Id,
                    Dishes = x.DishCarts.Select(dc => new DishOfCart(dc.Dish.Id, dc.Dish.Name, dc.Dish.PhotoLinks.First(),dc.Count)).ToList()
                })
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Cart not found!");

            return cart;
        }

        public async Task<ICollection<CartResponse>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Carts.AsNoTracking()
                    .Select(x => new CartResponse
                    {
                        Id = x.Id,
                        Dishes = x.DishCarts.Select(dc => new DishOfCart(dc.Dish.Id, dc.Dish.Name, dc.Dish.PhotoLinks.First(), dc.Count)).ToList()
                    })
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

        private CartResponse ConvertCartToResponse(Cart cart)
        {
            CartResponse cartResponse = new CartResponse()
            {
                Id = cart.Id,
            };
            foreach (var dish in cart.DishCarts)
            {
                cartResponse.Dishes.Add(new DishOfCart(dish.Dish.Id, dish.Dish.Name, dish.Dish.PhotoLinks.First(), dish.Count));
            }
            return cartResponse;
        }
    }
}
