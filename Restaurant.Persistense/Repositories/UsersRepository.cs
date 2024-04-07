using Restaurant.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Restaurant.Core.Models;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.CustomExceptions;

namespace Restaurant.Persistense.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RestaurantDbContext _context;
        public UsersRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            if(_context.Users.Any(x => x.Email == user.Email || x.PhoneNum == user.PhoneNum)) {
                throw new UserAlreadyExistsException("User with these credits already exists!");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
        }
    }
}
