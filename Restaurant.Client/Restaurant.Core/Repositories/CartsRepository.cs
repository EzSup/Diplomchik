using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOs.ForCreate;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Repositories
{
    public class CartsRepository : RepositoryWithSave, ICartsRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public CartsRepository(RestaurantDbContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public async Task<int> Create(CartForCreateDto dto)
        {
            var cart = new Cart();
            await _dbContext.Carts.AddAsync(cart);
            await Save();
            return cart.Id;
        }

        public async Task<bool> Delete(int id)
        {
            await _dbContext.Carts.Where(x => x.Id == id)
                .ExecuteDeleteAsync();
            return true;
        }

        public async Task<Cart?> Get(int id)
        {
            return await _dbContext.Carts.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Cart>> GetAll()
        {
            return await _dbContext.Carts.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<bool> Update(Cart dto)
        {
            await _dbContext.Carts.ExecuteUpdateAsync(c => c
            .SetProperty(c => c.DishCarts, c => dto.DishCarts));

            return true;
        }
    }
}
