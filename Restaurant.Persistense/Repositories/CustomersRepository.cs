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
    public class CustomersRepository : ICustomersRepository
    {
        private readonly RestaurantDbContext _context;

        public CustomersRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Customer entity)
        {
            if (!_context.Users.Any(x => x.Id == entity.UserId))
                throw new Exception("You have to create user first!");

            var model = new Customer()
            {
                Name = entity.Name,
                PhotoLink = entity.PhotoLink,
                CartId = entity.CartId,
                UserId = entity.UserId,
            };
            await _context.Customers.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Customers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ICollection<Customer>> GetByFilter(string? Name, Guid? UserId, Guid? CartId, Guid? ReviewId, Guid? BillId)
        {
            var query = _context.Customers.AsNoTracking();

            if(!String.IsNullOrWhiteSpace(Name))
                query = query.Where(x => x.Name.Contains(Name));

            if(UserId != null)
                query = query.Where(x => x.UserId == UserId);
            if(CartId != null)
                query = query.Where(x => x.CartId == CartId);
            if(ReviewId != null)
                query = query.Where(x => x.Reviews.Any(r => r.Id == ReviewId));
            if(UserId != null)
                query = query.Where(x => x.UserId == UserId);

            return await query.ToListAsync();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Reviews)
                .Include(x => x.Cart)
                .Include(x => x.Bills)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Customer not found!");
        }

        public async Task<ICollection<Customer>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Customers.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new NotImplementedException();
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Customers
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Customer entity)
        {
            return await _context.Customers
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, entity.Name)
                    .SetProperty(r => r.PhotoLink, entity.PhotoLink)
                    .SetProperty(r => r.CartId, entity.CartId)
                    .SetProperty(r => r.UserId, entity.UserId)) == 1;
        }
    }
}
